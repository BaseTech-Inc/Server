using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Marcador.Queries.GetMarcadoresById
{
    public class GetMarcadoresByIdQuery
    {
        public string UserId { get; set; }

        public string Id { get; set; }
    }

    public class GetMarcadoresByIdQueryHandler : IGetMarcadoresByIdQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetMarcadoresByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<Marcadores> Handle(GetMarcadoresByIdQuery request)
        {
            try
            {
                var entityUsuario = _context.Usuario
                    .Where(x => x.Id == request.UserId).FirstOrDefault();

                var entity = _context.Marcadores
                    .Where(x => x.Usuario == entityUsuario)
                        .Where(x => x.Id == request.Id)
                            .Include(e => e.Ponto)
                                    .ToList()
                                        .FirstOrDefault();

                return new Response<Marcadores>(data: entity);
            }
            catch
            {
                return new Response<Marcadores>(message: $"erro para obter");
            }
        }
    }
}
