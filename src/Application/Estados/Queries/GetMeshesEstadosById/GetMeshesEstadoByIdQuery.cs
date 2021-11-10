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

namespace Application.Estados.Queries.GetMeshesEstadosById
{
    public class GetMeshesEstadoByIdQuery
    {
        public string Id { get; set; }
    }

    public class GetMeshesEstadoByIdQueryHandler : IGetMeshesEstadoByIdQueryHandler
    {
        private readonly IApplicationDbContext _context;

        public GetMeshesEstadoByIdQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public Response<EstadoVm> Handle(GetMeshesEstadoByIdQuery request)
        {
            try
            {
                var listEncode = new List<String>();

                var entityPoligonoEstado = _context.PoligonoEstado
                    .Where(x => x.Estado.Id == request.Id)
                        .Include(e => e.Poligono)
                            .ToList();

                foreach (var polygon in entityPoligonoEstado)
                {
                    var pontoList = new List<Ponto>();

                    var entityPoligono = _context.Poligono
                        .Where(x => x == polygon.Poligono)
                            .ToList()
                                .FirstOrDefault();

                    var entityPoligonoPontos = _context.PoligonoPonto
                        .Where(x => x.Poligono == entityPoligono)
                            .Include(e => e.Ponto)
                                .OrderBy(x => x.Ponto.Count)
                                    .ToList();

                    foreach (var entityPoligonoPonto in entityPoligonoPontos)
                    {
                        pontoList.Add(entityPoligonoPonto.Ponto);
                    }

                    var encodeCoordinate = GooglePoint.EncodeCoordinate(pontoList).Replace(@"\\", @"\");

                    listEncode.Add(encodeCoordinate);
                }

                var estadoVm = new EstadoVm
                {
                    EncodePoints = listEncode
                };

                return new Response<EstadoVm>(
                    data: estadoVm,
                    message: "See https://developers.google.com/maps/documentation/utilities/polylinealgorithm");
            }
            catch
            {
                return new Response<EstadoVm>(message: $"erro para obter");
            }
        }
    }
}
