using System;
using System.Collections.Generic;
using System.Linq;

using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Alertas.Queries.GetAlertasByDate
{
    public class GetAlertasByDateQuery
    {
        public DateTime date { get; set; }
    }

    public class GetAlertasByDateQueryHandler : IGetAlertasByDateQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetAlertasByDateQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<IList<Alerta>> Handle(GetAlertasByDateQuery request)
        {
            try
            {
                var entity = _context.Alerta
                    .Where(x => x.TempoInicio.Year == request.date.Year)
                        .Where(x => x.TempoInicio.Month == request.date.Month)
                            .Where(x => x.TempoInicio.Day == request.date.Day)
                                .ToList();

                return new Response<IList<Alerta>>(data: entity);
            }
            catch
            {
                return new Response<IList<Alerta>>(message: $"error to get");
            }
        }
    }
}
