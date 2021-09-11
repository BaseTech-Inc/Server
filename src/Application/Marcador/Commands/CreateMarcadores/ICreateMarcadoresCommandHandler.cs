using System.Threading.Tasks;

using Application.Common.Models;

namespace Application.Marcador.Commands.CreateMarcadores
{
    public interface ICreateMarcadoresCommandHandler
    {
        Task<Response<string>> Handle(CreateMarcadoresCommand request);
    }
}
