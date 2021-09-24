using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class PontoRisco
    {
        public string Id { get; set; }

        public string Descricao { get; set; }

        public Ponto Ponto { get; set; }
    }
}
