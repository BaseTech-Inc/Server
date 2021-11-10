using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cidades.Queries.GetCidadesWithPagination
{
    public class GetCidadesWithPaginationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetCidadesWithPaginationQueryHandler : IGetCidadesWithPaginationQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetCidadesWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<PaginatedList<Cidade>>> Handle(GetCidadesWithPaginationQuery request)
        {
            try
            {
                var entity = _context.Cidade
                    .Include(e => e.Estado)
                        .Include(e => e.Estado.Pais)
                            .OrderBy(x => x.Nome);

                var entityPagination = await PaginatedList<Cidade>.CreateAsync(entity, request.PageNumber, request.PageSize);

                return new Response<PaginatedList<Cidade>>(data: entityPagination);
            }
            catch
            {
                return new Response<PaginatedList<Cidade>>(message: $"erro para obter");
            }
        }
    }
}
