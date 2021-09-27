using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq;

using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
            Database.SetCommandTimeout(180);
        }

        public DbSet<Alerta> Alerta { get; set; }
        public DbSet<Cidade> Cidade { get; set; }
        public DbSet<Distrito> Distrito { get; set; }
        public DbSet<Estado> Estado { get; set; }
        public DbSet<HistoricoPrevisao> HistoricoPrevisao { get; set; }
        public DbSet<HistoricoUsuario> HistoricoUsuario { get; set; }
        public DbSet<Marcadores> Marcadores { get; set; }
        public DbSet<Pais> Pais { get; set; }
        public DbSet<Poligono> Poligono { get; set; }
        public DbSet<PoligonoCidade> PoligonoCidade { get; set; }
        public DbSet<PoligonoDistrito> PoligonoDistrito { get; set; }
        public DbSet<PoligonoEstado> PoligonoEstado { get; set; }
        public DbSet<PoligonoPais> PoligonoPais { get; set; }
        public DbSet<PoligonoPonto> PoligonoPonto { get; set; }
        public DbSet<Ponto> Ponto { get; set; }
        public DbSet<PontoRisco> PontoRisco { get; set; }
        public DbSet<TipoUsuario> TipoUsuario { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var keysProperties = builder.Model.GetEntityTypes().Select(x => x.FindPrimaryKey()).SelectMany(x => x.Properties);
            foreach (var property in keysProperties)
            {
                property.ValueGenerated = ValueGenerated.OnAdd;
            }

            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.NormalizedEmail).HasMaxLength(85));
            builder.Entity<ApplicationUser>(entity => entity.Property(m => m.NormalizedUserName).HasMaxLength(85));
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityRole>(entity => entity.Property(m => m.NormalizedName).HasMaxLength(85));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.ProviderKey).HasMaxLength(85));
            builder.Entity<IdentityUserLogin<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityUserRole<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.LoginProvider).HasMaxLength(85));
            builder.Entity<IdentityUserToken<string>>(entity => entity.Property(m => m.Name).HasMaxLength(85));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityUserClaim<string>>(entity => entity.Property(m => m.UserId).HasMaxLength(85));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.Id).HasMaxLength(85));
            builder.Entity<IdentityRoleClaim<string>>(entity => entity.Property(m => m.RoleId).HasMaxLength(85));
        }
    }
}
