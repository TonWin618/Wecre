using MediatR;

namespace IdentityService.Domain.Events
{
    public record UserCreateEvent(string userName) :INotification;
}
