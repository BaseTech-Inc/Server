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

        Task<Response<string>> ChangePasswordWithIdAsync(string userId, string oldPassword, string newPassword);

        Task<Response<IDictionary<string, string>>> GetBasicProfile(string userId);
    }
}
