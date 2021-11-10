using System;
using System.Linq;
using System.Threading.Tasks;

using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.HistoricoUsuarios.Commands.CreateHistorico
{
    public class CreateHistoricoCommand
    {
        public string UserId { get; set; }

        public DateTime TempoChegada { get; set; }

        public DateTime TempoPartida { get; set; }

        public double DistanciaPercurso { get; set; }

        public string Distrito { get; set; }

        public string Cidade { get; set; }

        public string Estado { get; set; }

        public string Pais { get; set; }

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

            var entityDistrito = _context.Distrito
                .Where(x => EF.Functions.Like(x.Nome, "%" + request.Distrito + "%"))
                    .Where(x => EF.Functions.Like(x.Cidade.Nome, "%" + request.Cidade + "%"))
                        .Where(x => EF.Functions.Like(x.Cidade.Estado.Nome, "%" + request.Estado + "%"))
                            .Where(x => EF.Functions.Like(x.Cidade.Estado.Pais.Nome, "%" + request.Pais + "%"))
                                .Include(e => e.Cidade)
                                    .Include(e => e.Cidade.Estado)
                                        .Include(e => e.Cidade.Estado.Pais)
                                            .OrderBy(x => x.Nome)
                                                .ToList()
                                                    .FirstOrDefault();

            var entityHistorico = new HistoricoUsuario
            {
                DistanciaPercurso = request.DistanciaPercurso,
                TempoChegada = request.TempoChegada,
                TempoPartida = request.TempoPartida,
                Usuario = entityUsuario,
                Rota = request.Rota,
                Distrito = entityDistrito
            };

            try
            {
                _context.HistoricoUsuario.Add(entityHistorico);

                _context.SaveChanges();

                return new Response<string>(data: entityHistorico.Id.ToString());
            }
            catch
            {
                return new Response<string>(message: $"erro para criar: ${ entityHistorico.Id }");
            }
        }
    }
}
