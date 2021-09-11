using System.Threading.Tasks;

using Application.GeoJson.Features;
using Application.GeoJson.Geometry;

namespace Application.Common.Interfaces
{
    public interface IMeshesService
    {
        Task<Feature<TGeometry>> ProcessMeshes<TGeometry>(string path, string identifier)
            where TGeometry : IGeometryObject;

        Task<Feature<IGeometryObject>> ProcessMeshes(string path, string identifier);
    }
}
