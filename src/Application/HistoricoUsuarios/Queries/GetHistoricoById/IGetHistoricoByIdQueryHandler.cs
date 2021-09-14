using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Queries.GetHistoricoById
{
    public interface IGetHistoricoByIdQueryHandler
    {
        Response<HistoricoUsuario> Handle(GetHistoricoByIdQuery request);
    }
}
