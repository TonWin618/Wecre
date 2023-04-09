using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using System.Text;

namespace Common.EventBus.RabbitMQ;

internal class RabbitMQEventBus : IEventBus, IDisposable
{
    private IModel consumerChannel;
    private readonly string exchangeName;
    private string queueName;
    private readonly RabbitMQConnection persistentConnection;
    private readonly SubscriptionsManager subsManager;
    private readonly IServiceProvider serviceProvider;
    private readonly IServiceScope serviceScope;

    public RabbitMQEventBus(RabbitMQConnection persistentConnection,
        IServiceScopeFactory serviceProviderFactory, string exchangeName, string queueName)
    {
        this.persistentConnection = persistentConnection ?? throw new ArgumentNullException(nameof(persistentConnection));
        this.subsManager = new SubscriptionsManager();
        this.exchangeName = exchangeName;
        this.queueName = queueName;
        this.serviceScope = serviceProviderFactory.CreateScope();
        this.serviceProvider = serviceScope.ServiceProvider;
        this.consumerChannel = CreateConsumerChannel();
        this.subsManager.OnEventRemoved += SubsManager_OnEventRemoved; ;
    }

    private void SubsManager_OnEventRemoved(object? sender, string eventName)
    {
        if (!persistentConnection.IsConnected)
        {
            persistentConnection.TryConnect();
        }

        using (var channel = persistentConnection.CreateModel())
        {
            channel.QueueUnbind(queue: queueName,
                exchange: exchangeName,
                routingKey: eventName);

            if (subsManager.IsEmpty())
            {
                queueName = string.Empty;
                consumerChannel.Close();
            }
        }
    }

    public void Publish(string eventName, object? eventData)
    {
        if (!persistentConnection.IsConnected)
        {
            persistentConnection.TryConnect();
        }
        using (var channel = persistentConnection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: exchangeName, type: "direct");

            byte[] body;
            if (eventData == null)
            {
                body = new byte[0];
            }
            else
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                body = JsonSerializer.SerializeToUtf8Bytes(eventData, eventData.GetType(), options);
            }
            var properties = channel.CreateBasicProperties();
            properties.DeliveryMode = 2; // persistent

            channel.BasicPublish(
                exchange: exchangeName,
                routingKey: eventName,
                mandatory: true,
                basicProperties: properties,
                body: body);
        }
    }

    public void Subscribe(string eventName, Type handlerType)
    {
        CheckHandlerType(handlerType);
        DoInternalSubscription(eventName);
        subsManager.AddSubscription(eventName, handlerType);
        StartBasicConsume();
    }

    private void DoInternalSubscription(string eventName)
    {
        var containsKey = subsManager.HasSubscriptionsForEvent(eventName);
        if (!containsKey)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }
            consumerChannel.QueueBind(queue: queueName,
                                exchange: exchangeName,
                                routingKey: eventName);
        }
    }

    private void CheckHandlerType(Type handlerType)
    {
        if (!typeof(IIntegrationEventHandler).IsAssignableFrom(handlerType))
        {
            throw new ArgumentException($"{handlerType} doesn't inherit from IIntegrationEventHandler", nameof(handlerType));
        }
    }

    public void Unsubscribe(string eventName, Type handlerType)
    {
        CheckHandlerType(handlerType);
        subsManager.RemoveSubscription(eventName, handlerType);
    }

    public void Dispose()
    {
        if (consumerChannel != null)
        {
            consumerChannel.Dispose();
        }
        subsManager.Clear();
        this.persistentConnection.Dispose();
        this.serviceScope.Dispose();
    }

    private void StartBasicConsume()
    {
        if (consumerChannel != null)
        {
            var consumer = new AsyncEventingBasicConsumer(consumerChannel);
            consumer.Received += Consumer_Received;
            consumerChannel.BasicConsume(
                queue: queueName,
                autoAck: false,
                consumer: consumer);
        }
    }

    private async Task Consumer_Received(object sender, BasicDeliverEventArgs eventArgs)
    {
        var eventName = eventArgs.RoutingKey;
        var message = Encoding.UTF8.GetString(eventArgs.Body.Span);
        try
        {
            await ProcessEvent(eventName, message);
            consumerChannel.BasicAck(eventArgs.DeliveryTag, multiple: false);
            
        }
        catch (Exception ex)
        {
            Debug.Fail(ex.ToString());
        }
    }

    private IModel CreateConsumerChannel()
    {
        if (!persistentConnection.IsConnected)
        {
            persistentConnection.TryConnect();
        }

        var channel = persistentConnection.CreateModel();
        channel.ExchangeDeclare(exchange: exchangeName,
                                type: "direct");

        channel.QueueDeclare(queue: queueName,
                             durable: true,
                             exclusive: false,
                             autoDelete: false,
                             arguments: null);

        channel.CallbackException += (sender, ea) =>
        {
            /*
            _consumerChannel.Dispose();
            _consumerChannel = CreateConsumerChannel();
            StartBasicConsume();*/
            Debug.Fail(ea.ToString());
        };

        return channel;
    }

    private async Task ProcessEvent(string eventName, string message)
    {
        if (subsManager.HasSubscriptionsForEvent(eventName))
        {
            var subscriptions = subsManager.GetHandlersForEvent(eventName);
            foreach (var subscription in subscriptions)
            {
                using var scope = this.serviceProvider.CreateScope();
                IIntegrationEventHandler? handler = scope.ServiceProvider.GetService(subscription) as IIntegrationEventHandler;
                if (handler == null)
                {
                    throw new ApplicationException($"Cannot create a service of type {subscription}");
                }
                await handler.Handle(eventName, message);
            }
        }
        else
        {
            string entryAsm = Assembly.GetEntryAssembly().GetName().Name;
            Debug.WriteLine($"No handler could be found to handle eventName={eventName}，entryAsm:{entryAsm}");
        }
    }
}
