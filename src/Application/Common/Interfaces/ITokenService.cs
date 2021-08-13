using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<(string tokenString, DateTime validTo)> GenerateTokenJWT(string usuarioId, string userId = null, string userName = null, string email = null);

        string DecodeTokenJwt(string part);

        Task<string> GenerateTokenEmail(string userId);
    }
}
