using System;
using System.Reflection;

namespace Application.GeoJson.Converters
{
    public static class TypeExtensions
    {
        public static bool IsAssignableFromType(this Type self, Type other)
        {
            return self.GetTypeInfo().IsAssignableFrom(other.GetTypeInfo());
        }

    }
}
