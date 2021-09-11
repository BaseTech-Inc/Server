using System.Threading.Tasks;

using Application.Common.Models;
using Application.Marcador.Commands.DeleteMarcadores;

namespace Application.Marcador.Commands.UpdateMarcadores
{
    public interface IDeleteMarcadoresCommandHandler
    {
        Task<Response<string>> Handle(DeleteMarcadoresCommand request);
    }
}
