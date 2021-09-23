using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Queries.GetHistoricoByDate
{
    public interface IGetHistoricoByDateQueryHandler
    {
        Response<IList<HistoricoUsuario>> Handle(GetHistoricoByDateQuery request);
    }
}
