using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PontosRisco.Queries.GetPontoRiscoWithPagination
{
    public interface IGetPontoRiscoWithPaginationQueryHandler
    {
        Task<Response<PaginatedList<PontoRisco>>> Handle(GetPontoRiscoWithPaginationQuery request);
    }
}
