using Common.Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Common.Infrastructure;
public static class MediatorExtensions
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator,DbContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
                .Entries<IDomainEvents>()
                .Where(x => x.Entity.GetDomainEvents().Any());

        var domainEvents = domainEntities
                .SelectMany(x => x.Entity.GetDomainEvents())
                .ToList();
        //ToList() is added for immediate loading, otherwise execution will be delayed by the time foreach is cleared by ClearDomainEvents()

        domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        foreach (var domainEvent in domainEvents)
        {
            await mediator.Publish(domainEvent);
        }
    }
}
