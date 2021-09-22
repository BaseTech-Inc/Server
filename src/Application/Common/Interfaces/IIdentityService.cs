using System.Threading.Tasks;

using Application.Common.Models;
using Application.Common.Security;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<LoginResponse>> AuthenticateAsync(string email, string password);

        Task<Response<string>> RegisterAsync(string username, string email, string password);

        Task<Response<string>> VerifyEmailAsync(string userId, string tokenEmail);

        Task<Response<string>> GeneretPasswordResetAsync(string email);

        Task<Response<string>> ChangePasswordAsync(string email, string token, string password);
    }
}
