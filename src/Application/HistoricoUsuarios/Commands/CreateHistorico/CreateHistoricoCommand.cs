using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.HistoricoUsuarios.Commands.CreateHistorico
{
    public class CreateHistoricoCommand
    {
        public string UserId { get; set; }

        public double LatitudeChegada { get; set; }

        public double LongitudeChegada { get; set; }

        public double LatitudePartida { get; set; }

        public double LongitudePartida { get; set; }

        public double DistanciaPercurso { get; set; }

        public DateTime Duracao { get; set; }

        public string Rota { get; set; }
    }

    public class CreateHistoricoCommandHandler : ICreateHistoricoCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public CreateHistoricoCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(CreateHistoricoCommand request)
        {
            var entityPontoChegada = new Ponto
            {
                Latitude = request.LatitudeChegada,
                Longitude = request.LongitudeChegada
            };

            var entityPontoPartida = new Ponto
            {
                Latitude = request.LatitudePartida,
                Longitude = request.LongitudePartida
            };

            var entityUsuario = _context.Usuario.Where(x => x.Id == request.UserId).FirstOrDefault();

            var entityHistorico = new HistoricoUsuario
            {
                DistanciaPercurso = request.DistanciaPercurso,
                Duracao = request.Duracao,
                PontoChegada = entityPontoChegada,
                PontoPartida = entityPontoPartida,
                Usuario = entityUsuario,
                Rota = request.Rota
            };

            try
            {
                _context.HistoricoUsuario.Add(entityHistorico);

                _context.SaveChanges();

                return new Response<string>(data: entityHistorico.Id.ToString());
            }
            catch
            {
                return new Response<string>(message: $"error while creating: ${ entityHistorico.Id }");
            }

            return null;
        }
    }
}
