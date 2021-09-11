using System.Collections.Generic;
using System.Threading.Tasks;

using Application.Common.Models;
using Application.Common.Security;

namespace Application.Common.Interfaces
{
    public interface IIdentityGetService
    {
        Task<Response<IList<UsuarioResponse>>> GetAllIdentity();
    }
}
