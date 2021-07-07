using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Marcador.Commands.UpdateMarcadores
{
    public interface IUpdateMarcadoresCommandHandler
    {
        Task<Response<Marcadores>> Handle(UpdateMarcadoresCommand request);
    }
}
