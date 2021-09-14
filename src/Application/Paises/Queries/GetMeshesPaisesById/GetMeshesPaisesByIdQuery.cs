using Application.Common.GooglePoints;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Paises.Queries.GetMeshesPaisesById
{
    public class GetMeshesPaisesByIdQuery
    {
        public string Id { get; set; }
    }

    public class GetMeshesPaisesByIdQueryHandler : IGetMeshesPaisesByIdQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetMeshesPaisesByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<PaisesVm> Handle(GetMeshesPaisesByIdQuery request)
        {
            try
            {
                var pontoList = new List<Ponto>();

                var entityPoligonosPais = _context.PoligonoPais
                    .Where(x => x.Pais.Id == request.Id)
                        .ToList();

                foreach (var entityPoligonoPais in entityPoligonosPais)
                {
                    var entityPoligono = _context.Poligono
                        .Where(x => x.Id == entityPoligonoPais.Poligono.Id)
                            .ToList()
                                .FirstOrDefault();

                    var entityPoligonoPontos = _context.PoligonoPonto
                        .Where(x => x.Poligono.Id == entityPoligono.Id)
                            .ToList();

                    foreach (var entityPoligonoPonto in entityPoligonoPontos)
                    {
                        var entityPonto = _context.Ponto
                           .Where(x => x.Id == entityPoligonoPonto.Ponto.Id)
                               .ToList()
                                   .FirstOrDefault();

                        pontoList.Add(entityPonto);
                    }
                }

                var encodeCoordinate = GooglePoint.EncodeCoordinate(pontoList);

                var paisesVm = new PaisesVm
                {
                    AlgorithmSee = "See https://developers.google.com/maps/documentation/utilities/polylinealgorithm",
                    EncodePoints = encodeCoordinate
                };

                return new Response<PaisesVm>(data: paisesVm);
            }
            catch
            {
                return new Response<PaisesVm>(message: $"error to get");
            }
        }
    }
}
