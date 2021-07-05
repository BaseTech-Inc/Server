using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PoligonoPonto
    {
        public string Id { get; set; }

        public Poligono Poligono { get; set; }

        public Ponto Ponto { get; set; }
    }
}
