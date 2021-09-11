using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

using Domain.Entities;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Usuario> Usuario { get; set; }
    }
}
