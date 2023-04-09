using Common.EventBus;

namespace IdentityService.WebAPI.EventHandlers
{
    [EventName("User.Login")]
    public class TestIntegrationEventHandler : IIntegrationEventHandler
    {
        public Task Handle(string eventName, string eventData)
        {
            Console.WriteLine($"{eventName}: {eventData}");
            return Task.CompletedTask;
        }
    }
}
