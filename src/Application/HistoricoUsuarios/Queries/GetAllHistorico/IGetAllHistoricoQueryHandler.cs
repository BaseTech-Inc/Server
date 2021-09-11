using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Queries.GetAllHistorico
{
    public interface IGetAllHistoricoQueryHandler
    {
        Response<IList<HistoricoUsuario>> Handle(GetAllHistoricoQuery request);
    }
}
