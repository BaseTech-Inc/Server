using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Distritos.Queries.GetDistritosWithPagination
{
    public interface IGetDistritosWithPaginationQueryHandler
    {
        Task<Response<PaginatedList<Distrito>>> Handle(GetDistritosWithPaginationQuery request);
    }
}
