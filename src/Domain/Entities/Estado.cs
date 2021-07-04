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
        public Localizacao Localizacao { get; set; }
        #endregion

        #region Pais
        public Pais Pais { get; set; }
        #endregion

        public string Nome { get; set; }

        public string Siglas { get; set; }
    }
}
