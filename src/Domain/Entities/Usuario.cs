using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Usuario
    {
        public string Id { get; set; }

        #region TipoUsuario
        public int IdTipoUsuaurio { get; set; }

        public TipoUsuario TipoUsuario { get; set; }
        #endregion

        public string ContaBancaria { get; set; }
    }
}
