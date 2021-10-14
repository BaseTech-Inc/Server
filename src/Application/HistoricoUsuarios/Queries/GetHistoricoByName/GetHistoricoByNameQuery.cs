using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Queries.GetHistoricoByName
{
    public class GetHistoricoByNameQuery
    {
        public string UserId { get; set; }

        public string Distrito { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Pais { get; set; }
    }

    public class GetHistoricoByNameQueryHandler : IGetHistoricoByNameQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetHistoricoByNameQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<HistoricoUsuario>> Handle(GetHistoricoByNameQuery request)
        {
            try
            {
                var entityUsuario = _context.Usuario
                    .Where(x => x.Id == request.UserId).FirstOrDefault();

                var entity = _context.HistoricoUsuario
                    .Where(x => x.Usuario == entityUsuario)
                        .Where(x => EF.Functions.Like(x.Distrito.Nome, "%" + request.Distrito + "%"))
                            .Where(x => EF.Functions.Like(x.Distrito.Cidade.Nome, "%" + request.Cidade + "%"))
                                .Where(x => EF.Functions.Like(x.Distrito.Cidade.Estado.Nome, "%" + request.Estado + "%"))
                                    .Where(x => EF.Functions.Like(x.Distrito.Cidade.Estado.Pais.Nome, "%" + request.Pais + "%"))
                                        .Include(x => x.Distrito)
                                            .Include(x => x.Distrito.Cidade)
                                                .Include(x => x.Distrito.Cidade.Estado)
                                                    .Include(x => x.Distrito.Cidade.Estado.Pais)
                                                        .ToList();

                return new Response<IList<HistoricoUsuario>>(data: entity);
            }
            catch
            {
                return new Response<IList<HistoricoUsuario>>(message: $"error to get");
            }
        }
    }
}
