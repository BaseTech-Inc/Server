using Application.Common.Models;
using Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IGoogleService
    {
        Task<Response<LoginResponse>> AuthenticateGoogleAsync(string idToken);
    }
}
