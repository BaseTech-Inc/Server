using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Estados.Queries.GetEstadosById
{
    public class GetEstadosByIdQuery
    {
        public string Id { get; set; }
    }

    public class GetEstadosByIdQueryHandler : IGetEstadosByIdQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetEstadosByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<Estado> Handle(GetEstadosByIdQuery request)
        {
            try
            {
                var entity = _context.Estado
                    .Where(x => x.Id == request.Id)
                        .Include(e => e.Pais)
                            .ToList()
                                .FirstOrDefault();

                return new Response<Estado>(data: entity);
            }
            catch
            {
                return new Response<Estado>(message: $"erro para obter");
            }
        }
    }
}
