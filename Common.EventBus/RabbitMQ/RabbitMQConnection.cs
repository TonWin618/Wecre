using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Common.EventBus.RabbitMQ;

internal class RabbitMQConnection
{
    private readonly IConnectionFactory connectionFactory;
    private IConnection connection;
    private bool disposed;
    private readonly object sync_root = new object();

    public RabbitMQConnection(IConnectionFactory connectionFactory)
    {
        this.connectionFactory = connectionFactory;
    }

    public bool IsConnected
    {
        get
        {
            return connection != null && connection.IsOpen && !disposed;
        }
    }

    public IModel CreateModel()
    {
        if (!IsConnected)
        {
            throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");
        }
        return connection.CreateModel();
    }

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        connection.Dispose();
    }

    public bool TryConnect()
    {
        lock (sync_root)
        {
            connection = connectionFactory.CreateConnection();
            if (IsConnected)
            {
                connection.ConnectionShutdown += OnConnectionShutdown;
                connection.CallbackException += OnCallbackException;
                connection.ConnectionBlocked += OnConnectionBlocked;
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs e)
    {
        if (disposed) return;
        TryConnect();
    }

    void OnCallbackException(object sender, CallbackExceptionEventArgs e)
    {
        if (disposed) return;
        TryConnect();
    }

    void OnConnectionShutdown(object sender, ShutdownEventArgs reason)
    {
        if (disposed) return;
        TryConnect();
    }
}
