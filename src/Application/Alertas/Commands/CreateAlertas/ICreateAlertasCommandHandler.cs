using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Alertas.Commands.CreateAlertas
{
    public interface ICreateAlertasCommandHandler
    {
        Task<Response<string>> Handle(CreateAlertasCommand request);
    }
}
