using MediatR;

namespace IdentityService.Domain.Events
{
    public record TestDomainEvent(string userName) :INotification;
}
