using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                        .Where(x => x.TempoPartida.Year == request.date.Year)
                            .Where(x => x.TempoPartida.Month == request.date.Month)
                                .Where(x => x.TempoPartida.Day == request.date.Day)
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
