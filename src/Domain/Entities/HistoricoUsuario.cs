using System;

namespace Domain.Entities
{
    public class HistoricoUsuario
    {
        public string Id { get; set; }

        public Usuario Usuario { get; set; }

        public Ponto PontoChegada { get; set; }

        public Ponto PontoPartida { get; set; }

        public double DistanciaPercurso { get; set; }

        public DateTime Duracao { get; set; }

        public string Rota { get; set; }
    }
}
