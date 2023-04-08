using IdentityService.Domain.Events;
using MediatR;

namespace IdentityService.WebAPI.EventHandlers
{
    public class UserCreateEventHandler : INotificationHandler<UserCreateEvent>
    {
        public Task Handle(UserCreateEvent notification, CancellationToken cancellationToken)
        {
            Console.WriteLine($"New User {notification.userName}");
            return Task.CompletedTask;
        }
    }
}
