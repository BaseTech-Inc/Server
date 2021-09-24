using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PontosRisco.Commands.UpdatePontoRisco
{
    public interface IUpdatePontoRiscoCommandHandler
    {
        Task<Response<PontoRisco>> Handle(UpdatePontoRiscoCommand request);
    }
}
