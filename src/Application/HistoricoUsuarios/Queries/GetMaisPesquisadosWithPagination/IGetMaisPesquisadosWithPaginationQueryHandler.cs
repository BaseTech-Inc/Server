using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Queries.GetHistoricoWithPagination
{
    public interface IGetMaisPesquisadosWithPaginationQueryHandler
    {
        Task<Response<PaginatedList<Distrito>>> Handle(GetMaisPesquisadosWithPaginationQuery request);
    }
}
