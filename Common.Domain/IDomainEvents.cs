using MediatR;

namespace Common.Domain;
public interface IDomainEvents
{
    List<INotification> DomainEvents { get; set; }
    IEnumerable<INotification> GetDomainEvents()
    {
        return DomainEvents;
    }
    void AddDomainEvent(INotification eventItem)
    {
        DomainEvents.Add(eventItem);
    }
    /// <summary>
    /// If this element already exists, skip it, otherwise increment it. To avoid firing multiple times for the same event
    /// </summary>
    /// <param name="eventItem"></param>
    void AddDomainEventIfAbsent(INotification eventItem)
    {
        if (!DomainEvents.Contains(eventItem))
        {
            DomainEvents.Add(eventItem);
        }
    }
    void ClearDomainEvents()
    {
        DomainEvents.Clear();
    }
}