using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Marcador.Commands.UpdateMarcadores
{
    public class UpdateMarcadoresCommand
    {
        public string Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string UserId { get; set; }

        public string Nome { get; set; }
    }

    public class UpdateMarcadoresCommandHandler : IUpdateMarcadoresCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public UpdateMarcadoresCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<Marcadores>> Handle(UpdateMarcadoresCommand request)
        {
            var entityUsuario = _context.Usuario.Where(x => x.Id == request.UserId).FirstOrDefault();

            var entity = _context.Marcadores.Where(x => x.Id == request.Id).Where(x => x.Usuario == entityUsuario).FirstOrDefault();

            if (entity != null)
            {
                try
                {
                    entity.Nome = request.Nome;
                    entity.Ponto = new Ponto()
                    {
                        Latitude = request.Latitude,
                        Longitude = request.Longitude
                    };

                    _context.SaveChanges();

                    return new Response<Marcadores>(data: entity);
                }
                catch
                {
                    return new Response<Marcadores>(message: $"error while updating: ${ entity.Id }");
                }
            }
            else
            {
                return new Response<Marcadores>(message: $"no ${ entity.Id } was found ");
            }
        }
    }
}
