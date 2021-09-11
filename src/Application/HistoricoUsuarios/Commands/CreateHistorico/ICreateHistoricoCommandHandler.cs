using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Commands.CreateHistorico
{
    public interface ICreateHistoricoCommandHandler
    {
        Task<Response<string>> Handle(CreateHistoricoCommand request);
    }
}
