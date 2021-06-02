using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Marcadores
    {
        public string Id { get; set; }

        #region Localizacao
        public int IdLocal { get; set; }

        public Localizacao Localizacao { get; set; }
        #endregion

        #region Usuario
        public int IdUsuario { get; set; }

        public Usuario Usuario { get; set; }
        #endregion

        public string Nome { get; set; }
    }
}
