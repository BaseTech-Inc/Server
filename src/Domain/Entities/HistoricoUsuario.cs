using System;

namespace Domain.Entities
{
    public class HistoricoUsuario
    {
        public string Id { get; set; }

        public Usuario Usuario { get; set; }

        public Distrito Distrito { get; set; }

        public DateTime TempoChegada { get; set; }

        public DateTime TempoPartida { get; set; }

        public double DistanciaPercurso { get; set; }

        public string Rota { get; set; }
    }
}
