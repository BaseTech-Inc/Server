using System;

namespace Domain.Entities
{
    public class Alerta
    {
        public string Id { get; set; }

        public Ponto Ponto { get; set; }

        public Distrito Distrito { get; set; }

        public DateTime TempoInicio { get; set; }

        public DateTime TempoFinal { get; set; }

        public string Descricao { get; set; }

        public bool Transitividade { get; set; }

        public bool Atividade { get; set; }
    }
}
