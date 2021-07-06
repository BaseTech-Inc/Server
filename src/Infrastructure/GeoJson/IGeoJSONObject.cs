using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GeoJson
{
    public interface IGeoJSONObject
    {
        GeoJSONObjectType Type { get; }
    }
}
