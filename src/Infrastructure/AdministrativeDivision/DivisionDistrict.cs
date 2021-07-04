using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.AdministrativeDivision
{
    public class DivisionDistrict : DivisionBase
    {
        [JsonPropertyName("municipio")]
        public DivisionCounty County { get; set; }
    }
}
