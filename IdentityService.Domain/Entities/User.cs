using Microsoft.AspNetCore.Identity;

namespace IdentityService.Domain.Entities
{
    public class User:IdentityUser<Guid>
    {
        public DateTime CreationTime { get; init; }
        public User(string userName):base(userName)
        {
            Id = Guid.NewGuid();
            CreationTime = DateTime.UtcNow;
        }
    }
}
