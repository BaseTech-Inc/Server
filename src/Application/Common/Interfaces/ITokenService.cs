using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ITokenService
    {
        Task<(string tokenString, DateTime validTo)> GenerateTokenJWT(string userId);

        string DecodeTokenJwt(string part);

        Task<string> GenerateTokenEmail(string userId);
    }
}
