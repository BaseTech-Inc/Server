using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Flooding
{
    public class NominatimDto
    {
        public int place_id { get; set; }

        public string lat { get; set; }

        public string lon { get; set; }
    }
}
