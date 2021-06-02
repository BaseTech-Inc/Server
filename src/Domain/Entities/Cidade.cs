using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Cidade
    {
        public int IdCidade { get; set; }

        #region Localizacao
        public int IdLocal { get; set; }

        public Localizacao Localizacao { get; set; }
        #endregion

        #region Estado
        public int IdEstado { get; set; }

        public Estado Estado { get; set; }
        #endregion

        public string Nome { get; set; }
    }
}
