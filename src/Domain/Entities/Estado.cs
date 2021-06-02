using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Estado
    {
        public string Id { get; set; }

        #region Localizacao
        public int IdLocal { get; set; }

        public Localizacao Localizacao { get; set; }
        #endregion

        #region Pais
        public int IdPais { get; set; }

        public Pais Pais { get; set; }
        #endregion

        public string Nome { get; set; }
    }
}
