using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Paises.Queries.GetPaisesWithPagination
{
    public class GetPaisesWithPaginationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetPaisesWithPaginationQueryHandler : IGetPaisesWithPaginationQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetPaisesWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<PaginatedList<Pais>>> Handle(GetPaisesWithPaginationQuery request)
        {
            try
            {
                var entity = _context.Pais
                    .OrderBy(x => x.Nome);

                var entityPagination = await PaginatedList<Pais>.CreateAsync(entity, request.PageNumber, request.PageSize);

                return new Response<PaginatedList<Pais>>(data: entityPagination);
            }
            catch
            {
                return new Response<PaginatedList<Pais>>(message: $"erro para obter");
            }
        }
    }
}
