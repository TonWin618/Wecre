using Common.EventBus;
using IdentityService.Domain.Events;
using MediatR;

namespace IdentityService.WebAPI.EventHandlers;

public class TestDomainEventHandler : INotificationHandler<TestDomainEvent>
{
    private readonly IEventBus eventBus;
    public TestDomainEventHandler(IEventBus eventBus)
    {
        this.eventBus = eventBus;
    }
    public Task Handle(TestDomainEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine($"User Login {notification.userName}");
        eventBus.Publish("User.Login", new { username = notification.userName });
        return Task.CompletedTask;
    }
}
