using Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Commands.DeleteHistorico
{
    public interface IDeleteHistoricoCommandHandler
    {
        Task<Response<string>> Handle(DeleteHistoricoCommand request);
    }
}
