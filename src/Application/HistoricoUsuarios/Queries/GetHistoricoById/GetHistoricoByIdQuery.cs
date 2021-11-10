using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Queries.GetHistoricoById
{
    public class GetHistoricoByIdQuery
    {
        public string UserId { get; set; }

        public string Id { get; set; }
    }

    public class GetHistoricoByIdQueryHandler : IGetHistoricoByIdQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetHistoricoByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<HistoricoUsuario> Handle(GetHistoricoByIdQuery request)
        {
            try
            {
                var entityUsuario = _context.Usuario
                    .Where(x => x.Id == request.UserId).FirstOrDefault();

                var entity = _context.HistoricoUsuario
                    .Where(x => x.Usuario == entityUsuario)
                        .Where(x => x.Id == request.Id)
                            .Include(x => x.Distrito)
                                .Include(x => x.Distrito.Cidade)
                                    .Include(x => x.Distrito.Cidade.Estado)
                                        .Include(x => x.Distrito.Cidade.Estado.Pais)
                                            .ToList()
                                                .FirstOrDefault();

                return new Response<HistoricoUsuario>(data: entity);
            }
            catch
            {
                return new Response<HistoricoUsuario>(message: $"erro para obter");
            }
        }
    }
}
