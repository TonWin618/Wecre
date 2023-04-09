using Common.Domain;
using IdentityService.Domain.Events;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.Domain.Entities
{
    public class User: IdentityUser<Guid>, IDomainEvents
    {
        [NotMapped]
        public List<INotification> DomainEvents { get; set; }
        public DateTime CreationTime { get; init; }
        public User(string userName):base(userName)
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.UtcNow;
            DomainEvents = new List<INotification>();
        }

        public void TestEvent()
        {
            ((IDomainEvents)this).AddDomainEvent(new TestDomainEvent(this.UserName));
        }
    }
}
