using Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class TipoUsuario
    {
        public string Id { get; set; }

        public EnumTipoUsuario Nome { get; set; }
    }
}
