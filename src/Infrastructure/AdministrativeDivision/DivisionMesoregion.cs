using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.AdministrativeDivision
{
    public class DivisionMesoregion : DivisionBase
    {
        [JsonPropertyName("UF")]
        public DivisionState State { get; set; }
    }
}
