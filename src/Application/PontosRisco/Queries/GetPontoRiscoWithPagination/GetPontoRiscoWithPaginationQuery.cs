using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PontosRisco.Queries.GetPontoRiscoWithPagination
{
    public class GetPontoRiscoWithPaginationQuery
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetPontoRiscoWithPaginationQueryHandler : IGetPontoRiscoWithPaginationQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetPontoRiscoWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<PaginatedList<PontoRisco>>> Handle(GetPontoRiscoWithPaginationQuery request)
        {
            try
            {
                var entity = _context.PontoRisco
                    .Include(e => e.Ponto)
                        .Include(e => e.Distrito)
                            .Include(e => e.Distrito.Cidade)
                                .Include(e => e.Distrito.Cidade.Estado)
                                    .Include(e => e.Distrito.Cidade.Estado.Pais);

                var entityPagination = await PaginatedList<PontoRisco>.CreateAsync(entity, request.PageNumber, request.PageSize);

                return new Response<PaginatedList<PontoRisco>>(data: entityPagination);
            }
            catch
            {
                return new Response<PaginatedList<PontoRisco>>(message: $"erro para obter");
            }
        }
    }
}
