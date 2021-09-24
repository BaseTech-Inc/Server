using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PontosRisco.Commands.UpdatePontoRisco
{
    public class UpdatePontoRiscoCommand
    {
        public string Id { get; set; }

        public string Descricao { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class UpdatePontoRiscoCommandHandler : IUpdatePontoRiscoCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public UpdatePontoRiscoCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(UpdatePontoRiscoCommand request)
        {
            var entity = _context.PontoRisco
                .Where(x => x.Id == request.Id)
                    .FirstOrDefault();

            if (entity != null)
            {
                try
                {
                    entity.Descricao = request.Descricao;

                    entity.Ponto = new Ponto()
                    {
                        Latitude = request.Latitude,
                        Longitude = request.Longitude
                    };

                    _context.SaveChanges();

                    return new Response<string>(data: entity.Id.ToString());
                }
                catch
                {
                    return new Response<string>(message: $"error while updating: ${ entity.Id }");
                }
            }
            else
            {
                return new Response<string>(message: $"no ${ entity.Id } was found ");
            }
        }
    }
}
