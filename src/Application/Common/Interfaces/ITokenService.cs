using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<(string tokenString, DateTime validTo)> GenerateTokenJWT(string userId, string usuarioId);

        string DecodeTokenJwt(string part);

        Task<string> GenerateTokenEmail(string userId);
    }
}
