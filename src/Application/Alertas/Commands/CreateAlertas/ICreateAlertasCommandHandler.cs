using System.Threading.Tasks;

using Application.Common.Models;

namespace Application.Alertas.Commands.CreateAlertas
{
    public interface ICreateAlertasCommandHandler
    {
        Task<Response<string>> Handle(CreateAlertasCommand request);
    }
}
