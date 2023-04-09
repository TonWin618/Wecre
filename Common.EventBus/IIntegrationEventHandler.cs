namespace Common.EventBus;

public interface IIntegrationEventHandler
{
    Task Handle(string eventName, string eventData);
}