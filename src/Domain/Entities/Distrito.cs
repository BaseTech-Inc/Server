using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Distrito
    {
        public string Id { get; set; }

        #region Localizacao
        public Localizacao Localizacao { get; set; }
        #endregion

        #region Cidade
        public Cidade Cidade { get; set; }
        #endregion

        public string Nome { get; set; }
    }
}
