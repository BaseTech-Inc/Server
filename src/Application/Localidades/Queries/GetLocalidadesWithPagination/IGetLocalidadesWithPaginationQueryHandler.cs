using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Localidades.Queries.GetLocalidadesWithPagination
{
    public interface IGetLocalidadesWithPaginationQueryHandler
    {
        Response<PaginatedList<Distrito>> Handle(GetLocalidadesWithPaginationQuery request);
    }
}
