using System;

namespace Domain.Entities
{
    public class HistoricoPrevisao
    {
        public string Id { get; set; }

        public Distrito Distrito { get; set; }

        public DateTime Tempo { get; set; }

        public double Umidade { get; set; }

        public string Descricao { get; set; }

        public double TemperaturaMaxima { get; set; }

        public double TemperaturaMinima { get; set; }

        public double SensibilidadeTermica { get; set; }
    }
}
