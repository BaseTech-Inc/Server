using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PontosRisco.Commands.DeletePontoRisco
{
    public interface IDeletePontoRiscoCommandHandler
    {
        Task<Response<string>> Handle(DeletePontoRiscoCommand request);
    }
}
