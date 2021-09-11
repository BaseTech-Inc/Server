using System.Threading.Tasks;

using Application.Common.Models;
using Application.Common.Security;

namespace Application.Common.Interfaces
{
    public interface IGoogleService
    {
        Task<Response<LoginResponse>> AuthenticateGoogleAsync(string idToken);
    }
}
