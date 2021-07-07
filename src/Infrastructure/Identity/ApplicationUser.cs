using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
