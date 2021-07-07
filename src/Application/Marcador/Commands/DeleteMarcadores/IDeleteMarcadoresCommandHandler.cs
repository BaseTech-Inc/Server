using Application.Common.Models;
using Application.Marcador.Commands.DeleteMarcadores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Marcador.Commands.UpdateMarcadores
{
    public interface IDeleteMarcadoresCommandHandler
    {
        Task<Response<string>> Handle(DeleteMarcadoresCommand request);
    }
}
