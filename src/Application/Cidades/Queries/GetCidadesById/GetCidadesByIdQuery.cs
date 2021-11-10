using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Cidades.Queries.GetCidadesById
{
    public class GetCidadesByIdQuery
    {
        public string Id { get; set; }
    }

    public class GetCidadesByIdQueryHandler : IGetCidadesByIdQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetCidadesByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<Cidade> Handle(GetCidadesByIdQuery request)
        {
            try
            {
                var entity = _context.Cidade
                    .Where(x => x.Id == request.Id)
                        .Include(e => e.Estado)
                            .Include(e => e.Estado.Pais)
                                .ToList()
                                    .FirstOrDefault();

                return new Response<Cidade>(data: entity);
            }
            catch
            {
                return new Response<Cidade>(message: $"erro para obter");
            }
        }
    }
}
