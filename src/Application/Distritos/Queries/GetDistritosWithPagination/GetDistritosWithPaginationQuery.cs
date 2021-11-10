using Application.Cidades.Queries.GetCidadesWithPagination;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Distritos.Queries.GetDistritosWithPagination
{
    public class GetDistritosWithPaginationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetDistritosWithPaginationQueryHandler : IGetDistritosWithPaginationQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetDistritosWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<PaginatedList<Distrito>>> Handle(GetDistritosWithPaginationQuery request)
        {
            try
            {
                var entity = _context.Distrito
                    .Include(e => e.Cidade)
                        .Include(e => e.Cidade.Estado)
                            .Include(e => e.Cidade.Estado.Pais)
                                .OrderBy(x => x.Nome);

                var entityPagination = await PaginatedList<Distrito>.CreateAsync(entity, request.PageNumber, request.PageSize);

                return new Response<PaginatedList<Distrito>>(data: entityPagination);
            }
            catch
            {
                return new Response<PaginatedList<Distrito>>(message: $"erro para obter");
            }
        }
    }
}
