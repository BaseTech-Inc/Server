using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.GeoJson.Converters
{
    public static class TypeExtensions
    {
        public static bool IsAssignableFromType(this Type self, Type other)
        {
            return self.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());
        }

    }
}
