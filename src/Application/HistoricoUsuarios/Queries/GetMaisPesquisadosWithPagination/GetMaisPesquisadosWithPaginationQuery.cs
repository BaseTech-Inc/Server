using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Queries.GetHistoricoWithPagination
{
    public class GetMaisPesquisadosWithPaginationQuery
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string UserId { get; set; }
    }

    public class GetMaisPesquisadosWithPaginationQueryHandler : IGetMaisPesquisadosWithPaginationQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetMaisPesquisadosWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<PaginatedList<Distrito>>> Handle(GetMaisPesquisadosWithPaginationQuery request)
        {
            try
            {
                var entityUsuario = _context.Usuario
                    .Where(x => x.Id == request.UserId)
                        .FirstOrDefault();

                var entity = _context.HistoricoUsuario
                    .Where(x => x.Usuario == entityUsuario)
                        .Include(x => x.Distrito)
                            .Include(x => x.Distrito.Cidade)
                                .Include(x => x.Distrito.Cidade.Estado)
                                    .Include(x => x.Distrito.Cidade.Estado.Pais)
                                        .Select(x => x.Distrito)
                                            .Distinct()
                                                .OrderBy(x => x);

                var entityPagination = await PaginatedList<Distrito>.CreateAsync(entity, request.PageNumber, request.PageSize);

                return new Response<PaginatedList<Distrito>>(data: entityPagination);
            }
            catch
            {
                return new Response<PaginatedList<Distrito>>(message: $"error to get");
            }
        }
    }
}
