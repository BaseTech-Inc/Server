using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;

using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Marcador.Queries.GetAllMarcadores
{
    public class GetAllMarcadoresQuery
    {
        public string UserId { get; set; }
    }

    public class GetAllMarcadoresQueryHandler : IGetAllMarcadoresQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetAllMarcadoresQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<Marcadores>> Handle(GetAllMarcadoresQuery request)
        {
            try
            {
                var entityUsuario = _context.Usuario.Where(x => x.Id == request.UserId).FirstOrDefault();

                var entity = _context.Marcadores.Where(x => x.Usuario == entityUsuario).Include(e => e.Ponto).ToList();

                return new Response<IList<Marcadores>>(data: entity);
            }
            catch
            {
                return new Response<IList<Marcadores>>(message: $"erro para obter");
            }
        }
    }
}
