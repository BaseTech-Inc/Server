using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Marcador.Commands.CreateMarcadores
{
    public class CreateMarcadoresCommand
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string UserId { get; set; }

        public string Nome { get; set; }
    }

    public class CreateMarcadoresCommandHandler : ICreateMarcadoresCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public CreateMarcadoresCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(CreateMarcadoresCommand request)
        {
            var entityPonto = new Ponto
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };

            var entityUsuario = _context.Usuario.Where(x => x.Id == request.UserId).FirstOrDefault();

            var entityMarcador = new Marcadores
            {
                Nome = request.Nome,
                Usuario = entityUsuario,
                Ponto = entityPonto
            };

            try
            {
                _context.Marcadores.Add(entityMarcador);

                _context.SaveChanges();

                return new Response<string>(data: entityMarcador.Id.ToString());
            }
            catch
            {
                return new Response<string>(message: $"error while creating: ${ entityMarcador.Id }");
            }

            return null;
        }
    }
}
