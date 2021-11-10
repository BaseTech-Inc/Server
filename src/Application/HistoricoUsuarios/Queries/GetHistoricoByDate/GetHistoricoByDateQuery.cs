using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.HistoricoUsuarios.Queries.GetHistoricoByDate
{
    public class GetHistoricoByDateQuery
    {
        public string UserId { get; set; }

        public DateTime date { get; set; }
    }

    public class GetHistoricoByDateQueryHandler : IGetHistoricoByDateQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetHistoricoByDateQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<HistoricoUsuario>> Handle(GetHistoricoByDateQuery request)
        {
            try
            {
                var entityUsuario = _context.Usuario
                    .Where(x => x.Id == request.UserId).FirstOrDefault();

                var entity = _context.HistoricoUsuario
                    .Where(x => x.Usuario == entityUsuario)
                        .Where(x => x.TempoPartida.Date == request.date.Date)
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
