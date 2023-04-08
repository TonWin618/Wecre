using Common.Domain;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityService.Domain.Entities
{
    public class Role:IdentityRole<Guid>
    {
        public Role() 
        {
            User user = new("");
            this.Id = Guid.NewGuid();
        }
    }
}
