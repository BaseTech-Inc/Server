using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HistoricoPrevisao
    {
        public int IdHistoricoPrevisao { get; set; }

        #region Distrito
        public int IdDistrito { get; set; }

        public Distrito Distrito { get; set; }
        #endregion

        public DateTime Tempo { get; set; }

        public double Umidade { get; set; }

        public string Descricao { get; set; }

        public double TemperaturaMaxima { get; set; }

        public double TemperaturaMinima { get; set; }

        public double SensibilidadeTermica { get; set; }
    }
}
