using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Localidades.Queries.GetLocalidadesWithPagination
{
    public class GetLocalidadesWithPaginationQuery
    {
        public string namePais { get; set; }

        public string nameEstado { get; set; }

        public string nameCidade { get; set; }

        public string nameDistrito { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }

    public class GetLocalidadesWithPaginationQueryHandler : IGetLocalidadesWithPaginationQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetLocalidadesWithPaginationQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<PaginatedList<Distrito>> Handle(GetLocalidadesWithPaginationQuery request)
        {
            try
            {
                var entity = _context.Distrito
                    .Where(x => EF.Functions.Like(x.Nome, "%" + request.nameDistrito + "%"))
                        .Where(x => EF.Functions.Like(x.Cidade.Nome, "%" + request.nameCidade + "%"))
                            .Where(x => EF.Functions.Like(x.Cidade.Estado.Nome, "%" + request.nameEstado + "%"))
                                .Where(x => EF.Functions.Like(x.Cidade.Estado.Pais.Nome, "%" + request.namePais + "%"))
                                    .Include(e => e.Cidade)
                                        .Include(e => e.Cidade.Estado)
                                            .Include(e => e.Cidade.Estado.Pais)
                                                .ToList();

                var entityPagination = PaginatedList<Distrito>.Create(entity, request.PageNumber, request.PageSize);

                return new Response<PaginatedList<Distrito>>(data: entityPagination);
            }
            catch
            {
                return new Response<PaginatedList<Distrito>>(message: $"erro para obter");
            }
        }
    }
}
