using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.HistoricoUsuarios.Queries.GetAllHistorico
{
    public class GetAllHistoricoQuery
    {
        public string UserId { get; set; }
    }

    public class GetAllHistoricoQueryHandler : IGetAllHistoricoQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetAllHistoricoQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<HistoricoUsuario>> Handle(GetAllHistoricoQuery request)
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
                                        .ToList();

                return new Response<IList<HistoricoUsuario>>(data: entity);
            }
            catch
            {
                return new Response<IList<HistoricoUsuario>>(message: $"erro para obter");
            }
        }
    }
}
