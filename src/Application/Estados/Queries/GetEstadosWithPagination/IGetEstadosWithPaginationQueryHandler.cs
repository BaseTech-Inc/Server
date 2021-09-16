using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Estados.Queries.GetEstadosWithPagination
{
    public interface IGetEstadosWithPaginationQueryHandler
    {
        Task<Response<PaginatedList<Estado>>> Handle(GetEstadosWithPaginationQuery request);
    }
}
