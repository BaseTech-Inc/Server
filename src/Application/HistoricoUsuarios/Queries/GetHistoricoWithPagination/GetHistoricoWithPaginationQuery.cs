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
    public class GetHistoricoWithPaginationQuery
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string UserId { get; set; }
    }

    public class GetHistoricoWithPaginationQueryHandler : IGetHistoricoWithPaginationQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetHistoricoWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<PaginatedList<HistoricoUsuario>>> Handle(GetHistoricoWithPaginationQuery request)
        {
            try
            {
                var entityUsuario = _context.Usuario
                    .Where(x => x.Id == request.UserId)
                        .FirstOrDefault();

                var entity = _context.HistoricoUsuario
                    .Where(x => x.Usuario == entityUsuario)
                        .OrderBy(x => x.TempoChegada);

                var entityPagination = await PaginatedList<HistoricoUsuario>.CreateAsync(entity, request.PageNumber, request.PageSize);

                return new Response<PaginatedList<HistoricoUsuario>>(data: entityPagination);
            }
            catch
            {
                return new Response<PaginatedList<HistoricoUsuario>>(message: $"error to get");
            }
        }
    }
}
