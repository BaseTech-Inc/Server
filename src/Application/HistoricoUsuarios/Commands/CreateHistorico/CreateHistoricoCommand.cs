using System;
using System.Linq;
using System.Threading.Tasks;

using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.HistoricoUsuarios.Commands.CreateHistorico
{
    public class CreateHistoricoCommand
    {
        public string UserId { get; set; }

        public DateTime TempoChegada { get; set; }

        public DateTime TempoPartida { get; set; }

        public double DistanciaPercurso { get; set; }

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
            var entityUsuario = _context.Usuario.Where(x => x.Id == request.UserId).FirstOrDefault();

            var entityHistorico = new HistoricoUsuario
            {
                DistanciaPercurso = request.DistanciaPercurso,
                TempoChegada = request.TempoChegada,
                TempoPartida = request.TempoPartida,
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
        }
    }
}
