using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PontosRisco.Queries.GetPontoRiscoById
{
    public class GetPontoRiscoByIdQuery
    {
        public string Id { get; set; }
    }

    public class GetPontoRiscoByIdQueryHandler : IGetPontoRiscoByIdQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetPontoRiscoByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<PontoRisco> Handle(GetPontoRiscoByIdQuery request)
        {
            try
            {
                var entity = _context.PontoRisco
                    .Where(x => x.Id == request.Id)
                        .ToList()
                            .FirstOrDefault();

                return new Response<PontoRisco>(data: entity);
            }
            catch
            {
                return new Response<PontoRisco>(message: $"error to get");
            }
        }
    }
}
