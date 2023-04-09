using Common.EventBus.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Reflection;

namespace Common.EventBus
{
    public static class ServicesCollectionExtensions
    {
        public static IServiceCollection AddEventBus(this IServiceCollection services, string queueName,
            params Assembly[] assemblies)
        {
            return AddEventBus(services, queueName, assemblies.ToList());
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, string queueName,
            IEnumerable<Assembly> assemblies)
        {
            List<Type> eventHandlers = new List<Type>();
            foreach (var asm in assemblies)
            {
                var types = asm.GetTypes().Where(t => t.IsAbstract == false && t.IsAssignableTo(typeof(IIntegrationEventHandler)));
                eventHandlers.AddRange(types);
            }
            return AddEventBus(services, queueName, eventHandlers);
        }
        public static IServiceCollection AddEventBus(this IServiceCollection services, string queueName, IEnumerable<Type> eventHandlerTypes)
        {
            foreach (Type type in eventHandlerTypes)
            {
                services.AddScoped(type, type);
            }

            services.AddSingleton<IEventBus>(sp =>
            {
                var optionMQ = sp.GetRequiredService<IOptions<IntegrationEventRabbitMQOptions>>().Value;
                var factory = new ConnectionFactory()
                {
                    HostName = optionMQ.HostName,

                    DispatchConsumersAsync = true
                };
                if(optionMQ.UserName!=null)
                {
                    factory.UserName = optionMQ.UserName;
                }
                if (optionMQ.Password != null)
                {
                    factory.Password = optionMQ.Password;
                }
                RabbitMQConnection mqConnection = new RabbitMQConnection(factory);
                var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var eventBus = new RabbitMQEventBus(mqConnection, serviceScopeFactory, optionMQ.ExchangeName, queueName);
                foreach (Type type in eventHandlerTypes)
                {
                    var eventNameAttrs = type.GetCustomAttributes<EventNameAttribute>();
                    if (eventNameAttrs.Any() == false)
                    {
                        throw new ApplicationException($"There shoule be at least one EventNameAttribute on {type}");
                    }
                    foreach (var eventNameAttr in eventNameAttrs)
                    {
                        eventBus.Subscribe(eventNameAttr.Name, type);
                    }
                }
                return eventBus;
            });
            return services;
        }
    }
}
