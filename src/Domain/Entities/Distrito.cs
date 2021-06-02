using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Distrito
    {
        public int IdDistrito { get; set; }

        #region Localizacao
        public int IdLocal { get; set; }

        public Localizacao Localizacao { get; set; }
        #endregion

        #region Cidade
        public int IdCidade { get; set; }

        public Cidade Cidade { get; set; }
        #endregion

        public string Nome { get; set; }
    }
}
