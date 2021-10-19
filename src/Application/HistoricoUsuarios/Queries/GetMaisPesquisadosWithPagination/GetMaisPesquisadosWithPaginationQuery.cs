using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Queries.GetMaisPesquisadosWithPagination
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

        public async Task<Response<PaginatedList<MaisPesquisadosDto>>> Handle(GetMaisPesquisadosWithPaginationQuery request)
        {
            try
            {
                var entityUsuario = _context.Usuario
                    .Where(x => x.Id == request.UserId)
                        .FirstOrDefault();

                var entities = _context.HistoricoUsuario
                    .Where(x => x.Usuario == entityUsuario)
                        .Include(x => x.Distrito)
                            .Select(s => s.Distrito)
                                .GroupBy(p => p.Id)
                                    .Select(g => new { Id = g.Key, Count = g.Count() })
                                        .ToList();

                var list = new List<MaisPesquisadosDto>();

                foreach (var entity in entities)
                {
                    list.Add(new MaisPesquisadosDto
                    {
                        Distrito = _context.Distrito
                            .Where(x => x.Id == entity.Id)
                                .Include(x => x.Cidade)
                                    .Include(x => x.Cidade.Estado)
                                        .Include(x => x.Cidade.Estado.Pais).FirstOrDefault(),
                        Count = entity.Count
                        });
                }

                var entityPagination = PaginatedList<MaisPesquisadosDto>.Create(list, request.PageNumber, request.PageSize);

                return new Response<PaginatedList<MaisPesquisadosDto>>(data: entityPagination);
            }
            catch
            {
                return new Response<PaginatedList<MaisPesquisadosDto>>(message: $"error to get");
            }
        }
    }
}
