using Application.Common.Models;
using Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<Response<LoginResponse>> AuthenticateAsync(string email, string password);

        Task<Response<string>> RegisterAsync(string username, string email, string password);

        Task<Response<string>> VerifyEmailAsync(string userId, string tokenEmail);
    }
}
