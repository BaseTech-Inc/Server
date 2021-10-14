using System.Collections.Generic;
using System.Threading.Tasks;

using Application.Common.Models;
using Application.Common.Security;
using Microsoft.AspNetCore.Http;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<LoginResponse>> AuthenticateAsync(string email, string password, HttpContext httpContext);

        Task<Response<string>> RegisterAsync(string username, string email, string password);

        Task<Response<LoginResponse>> RefreshToken(HttpContext httpContext);

        Task<Response<string>> RevokeToken(HttpContext httpContext, string token);

        Task<Response<string>> LogoutAsync(HttpContext httpContext);

        Task<Response<string>> VerifyEmailAsync(string userId, string tokenEmail);

        Task<Response<string>> GeneretPasswordResetAsync(string email);

        Task<Response<string>> ChangePasswordAsync(string email, string token, string password);

        Task<Response<string>> ChangePasswordWithIdAsync(string UserId, string oldPassword, string newPassword);

        Task<Response<IDictionary<string, string>>> GetBasicProfile(string UserId);

        Task<Response<string>> UpdateBasicProfile(string UserId, string UserName, string TipoUsuario);

        Task<Response<string>> DeleteAsync(string UserId);

        Task<Response<string>> GetProfileImage(string UserId);

        Task<Response<string>> UpdateProfileImage(string UserId, string base64Image);
    }
}
