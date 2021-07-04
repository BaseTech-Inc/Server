using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Alerta
    {
        public string Id { get; set; }

        #region Localizacao
        public Localizacao Localizacao { get; set; }
        #endregion

        #region Distrito
        public Distrito Distrito { get; set; }
        #endregion

        public DateTime Tempo { get; set; }

        public string Descricao { get; set; }

        public bool Transitividade { get; set; }

        public bool Atividade { get; set; }
    }
}
