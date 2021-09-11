using System.Threading.Tasks;

using Application.Common.Models;
using Domain.Entities;

namespace Application.Marcador.Commands.UpdateMarcadores
{
    public interface IUpdateMarcadoresCommandHandler
    {
        Task<Response<Marcadores>> Handle(UpdateMarcadoresCommand request);
    }
}
