using Application.Common.GooglePoints;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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
                var listEncode = new List<String>();

                var entityPoligonoPais = _context.PoligonoPais
                    .Where(x => x.Pais.Id == request.Id)
                        .Include(e => e.Poligono)
                            .ToList();

                foreach (var polygon in entityPoligonoPais)
                {
                    var pontoList = new List<Ponto>();

                    var entityPoligono = _context.Poligono
                        .Where(x => x == polygon.Poligono)
                            .ToList()
                                .FirstOrDefault();

                    var entityPoligonoPontos = _context.PoligonoPonto
                        .Where(x => x.Poligono == entityPoligono)
                            .Include(e => e.Ponto)
                                .ToList();

                    foreach (var entityPoligonoPonto in entityPoligonoPontos)
                    {
                        pontoList.Add(entityPoligonoPonto.Ponto);
                    }

                    var encodeCoordinate = GooglePoint.EncodeCoordinate(pontoList).Replace(@"\\", @"\");

                    listEncode.Add(encodeCoordinate);
                }

                var paisesVm = new PaisesVm
                {
                    EncodePoints = listEncode
                };

                return new Response<PaisesVm>(
                    data: paisesVm, 
                    message: "See https://developers.google.com/maps/documentation/utilities/polylinealgorithm");
            }
            catch
            {
                return new Response<PaisesVm>(message: $"error to get");
            }
        }
    }
}
