namespace Common.EventBus;

internal class SubscriptionsManager
{
    private readonly Dictionary<string, List<Type>> handlers = new Dictionary<string, List<Type>>();
    public event EventHandler<string> OnEventRemoved;

    public bool IsEmpty()
    {
       return !handlers.Keys.Any();
    }
    public void Clear()
    {
        handlers.Clear();
    }

    public void AddSubscription(string eventName,Type eventHandlerType)
    {
        if (!HasSubscriptionsForEvent(eventName))
        {
            handlers.Add(eventName, new List<Type>());
        }
        if (handlers[eventName].Contains(eventHandlerType))
        {
            throw new ArgumentException($"Handler Type {eventHandlerType} already registered for '{eventName}'", nameof(eventHandlerType));
        }
        handlers[eventName].Add(eventHandlerType);
    }

    public void RemoveSubscription(string eventName, Type handlerType)
    {
        handlers[eventName].Remove(handlerType);
        if (!handlers[eventName].Any())
        {
            handlers.Remove(eventName);
            OnEventRemoved?.Invoke(this, eventName);
        }
    }

    public IEnumerable<Type> GetHandlersForEvent(string eventName)
    {
        return handlers[eventName];
    }

    public bool HasSubscriptionsForEvent(string eventName)
    {
        return handlers.ContainsKey(eventName);
    }
}
