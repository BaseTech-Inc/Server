using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PoligonoPais
    {
        public string Id { get; set; }

        public Ponto Ponto { get; set; }

        public Pais Pais { get; set; }
    }
}
