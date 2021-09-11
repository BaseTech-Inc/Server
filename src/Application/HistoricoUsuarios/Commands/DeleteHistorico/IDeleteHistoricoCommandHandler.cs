using System.Threading.Tasks;

using Application.Common.Models;

namespace Application.HistoricoUsuarios.Commands.DeleteHistorico
{
    public interface IDeleteHistoricoCommandHandler
    {
        Task<Response<string>> Handle(DeleteHistoricoCommand request);
    }
}
