using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

using Domain.Entities;
using System;

namespace Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public virtual ICollection<Usuario> Usuario { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }
    }

    public class RefreshToken
    {
        public string Id { get; set; }

        public string Token { get; set; }

        public string UserId { get; set; }

        public DateTime ExpiryOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public string CreatedByIp { get; set; }

        public DateTime RevokedOn { get; set; }

        public string RevokedByIp { get; set; }
    }
}
