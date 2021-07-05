using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.AdministrativeDivision
{
    public class DivisionLocation
    {
        public class DivisionBase
        {
            [JsonPropertyName("id")]
            public int Id { get; set; }

            [JsonPropertyName("nome")]
            public string Name { get; set; }
        }

        public class DivisionCounty : DivisionBase
        {
            [JsonPropertyName("microrregiao")]
            public DivisionMicroregion Microregiao { get; set; }
        }

        public class DivisionDistrict : DivisionBase
        {
            [JsonPropertyName("municipio")]
            public DivisionCounty County { get; set; }
        }

        public class DivisionMesoregion : DivisionBase
        {
            [JsonPropertyName("UF")]
            public DivisionState State { get; set; }
        }

        public class DivisionMicroregion : DivisionBase
        {
            [JsonPropertyName("mesorregiao")]
            public DivisionMesoregion Mesoregion { get; set; }
        }

        public class DivisionState : DivisionBase
        {
            [JsonPropertyName("sigla")]
            public string Initials { get; set; }
        }
    }
}
