using Application.GeoJson.Features;
using Application.GeoJson.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IMeshesService
    {
        Task<Feature<TGeometry>> ProcessMeshes<TGeometry>(string path, string identifier)
            where TGeometry : IGeometryObject;

        Task<Feature<IGeometryObject>> ProcessMeshes(string path, string identifier);
    }
}
