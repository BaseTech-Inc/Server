using System.Threading.Tasks;

using Application.Common.Models;

namespace Application.HistoricoUsuarios.Commands.CreateHistorico
{
    public interface ICreateHistoricoCommandHandler
    {
        Task<Response<string>> Handle(CreateHistoricoCommand request);
    }
}
