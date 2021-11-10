using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Queries.GetHistoricoByNameWithPagination
{
    public class GetHistoricoByNameWithPaginationQuery
    {
        public string UserId { get; set; }

        public string Distrito { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Pais { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

    public class GetHistoricoByNameWithPaginationQueryHandler : IGetHistoricoByNameWithPaginationQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetHistoricoByNameWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<PaginatedList<HistoricoUsuario>>> Handle(GetHistoricoByNameWithPaginationQuery request)
        {
            try
            {
                var entityUsuario = _context.Usuario
                    .Where(x => x.Id == request.UserId)
                        .FirstOrDefault();

                var entity = _context.HistoricoUsuario
                    .Where(x => x.Usuario == entityUsuario)
                        .Where(x => x.Usuario == entityUsuario)
                            .Where(x => EF.Functions.Like(x.Distrito.Nome, "%" + request.Distrito + "%"))
                                .Where(x => EF.Functions.Like(x.Distrito.Cidade.Nome, "%" + request.Cidade + "%"))
                                    .Where(x => EF.Functions.Like(x.Distrito.Cidade.Estado.Nome, "%" + request.Estado + "%"))
                                        .Where(x => EF.Functions.Like(x.Distrito.Cidade.Estado.Pais.Nome, "%" + request.Pais + "%"))
                                            .Include(x => x.Distrito)
                                                .Include(x => x.Distrito.Cidade)
                                                    .Include(x => x.Distrito.Cidade.Estado)
                                                        .Include(x => x.Distrito.Cidade.Estado.Pais)
                                                            .OrderBy(x => x.TempoChegada);

                var entityPagination = await PaginatedList<HistoricoUsuario>.CreateAsync(entity, request.PageNumber, request.PageSize);

                return new Response<PaginatedList<HistoricoUsuario>>(data: entityPagination);
            }
            catch
            {
                return new Response<PaginatedList<HistoricoUsuario>>(message: $"erro para obter");
            }
        }
    }
}
