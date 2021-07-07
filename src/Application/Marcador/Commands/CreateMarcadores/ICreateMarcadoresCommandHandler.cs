using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Marcador.Commands.CreateMarcadores
{
    public interface ICreateMarcadoresCommandHandler
    {
        Task<Response<string>> Handle(CreateMarcadoresCommand request);
    }
}
