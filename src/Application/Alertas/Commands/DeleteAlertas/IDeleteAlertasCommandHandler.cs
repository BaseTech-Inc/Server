using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Alertas.Commands.DeleteAlertas
{
    public interface IDeleteAlertasCommandHandler
    {
        Task<Response<string>> Handle(DeleteAlertasCommand request);
    }
}
