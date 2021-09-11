using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IPlacesService
    {
        Task<List<T>> ProcessPlaces<T>(string path);
    }
}
