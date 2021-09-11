using System.Collections.Generic;

using Application.Common.Models;
using Domain.Entities;

namespace Application.HistoricoUsuarios.Queries.GetAllHistorico
{
    public interface IGetAllHistoricoQueryHandler
    {
        Response<IList<HistoricoUsuario>> Handle(GetAllHistoricoQuery request);
    }
}
