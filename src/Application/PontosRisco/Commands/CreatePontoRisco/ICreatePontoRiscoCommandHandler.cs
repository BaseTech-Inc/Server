using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PontosRisco.Commands.CreatePontoRisco
{
    public interface ICreatePontoRiscoCommandHandler
    {
        Task<Response<string>> Handle(CreatePontoRiscoCommand request);
    }
}
