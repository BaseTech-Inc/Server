using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Estados.Queries.GetEstadosWithPagination
{
    public class GetEstadosWithPaginationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetEstadosWithPaginationQueryHandler : IGetEstadosWithPaginationQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetEstadosWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<PaginatedList<Estado>>> Handle(GetEstadosWithPaginationQuery request)
        {
            try
            {
                var entity = _context.Estado
                    .Include(e => e.Pais)
                        .OrderBy(x => x.Nome);

                var entityPagination = await PaginatedList<Estado>.CreateAsync(entity, request.PageNumber, request.PageSize);

                return new Response<PaginatedList<Estado>>(data: entityPagination);
            }
            catch
            {
                return new Response<PaginatedList<Estado>>(message: $"error to get");
            }
        }
    }
}
