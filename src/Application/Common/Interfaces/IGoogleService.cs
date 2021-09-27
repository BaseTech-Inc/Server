using System.Threading.Tasks;

using Application.Common.Models;
using Application.Common.Security;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IGoogleService
    {
        Task<Response<LoginResponse>> AuthenticateGoogleAsync(string idToken, HttpContext httpContext);
    }
}
