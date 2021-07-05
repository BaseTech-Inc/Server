using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PoligonoEstado
    {
        public string Id { get; set; }

        public Poligono Poligono { get; set; }

        public Estado Estado { get; set; }
    }
}
