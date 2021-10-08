using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<(string tokenString, string refreshToken, DateTime validTo)> GenerateTokens(Usuario usuario, string userId, HttpContext httpContext);

        string DecodeTokenJwt(string part);

        Task<(string tokenString, DateTime validTo)> GenerateTokenJWT(string usuarioId, string userId = null, string userName = null, string email = null);

        Task<string> GenerateTokenEmail(string userId);

        Task<bool> RevokeRefreshToken(HttpContext httpContext, string token = null);
    }
}
