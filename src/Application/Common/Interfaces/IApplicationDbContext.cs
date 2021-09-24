using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
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

        int SaveChanges();
    }
}
