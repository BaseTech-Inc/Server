using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cidades.Queries.GetCidadesWithPagination
{
    public interface IGetCidadesWithPaginationQueryHandler
    {
        Task<Response<PaginatedList<Cidade>>> Handle(GetCidadesWithPaginationQuery request);
    }
}
