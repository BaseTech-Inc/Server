using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;

namespace Application.Alertas.Commands.CreateAlertas
{
    public class CreateAlertasCommand
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Distrito { get; set; }

        public DateTime TempoInicio { get; set; }

        public DateTime TempoFinal { get; set; }

        public string Descricao { get; set; }

        public bool Transitividade { get; set; }

        public bool Atividade { get; set; }
    }

    public class CreateAlertasCommandHandler : ICreateAlertasCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public CreateAlertasCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(CreateAlertasCommand request)
        {
            var entityPonto = new Ponto
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };

            var entityDistrito = _context.Distrito
                .Where(x => EF.Functions.Like(x.Nome, "%" + request.Distrito + "%"))
                    .Include(e => e.Cidade)
                        .Include(e => e.Cidade.Estado)
                            .Include(e => e.Cidade.Estado.Pais)
                                .OrderBy(x => x.Nome)
                                    .ToList()                                    
                                        .FirstOrDefault();

            var entityAlertas = new Alerta
            {
                Atividade = request.Atividade,
                Descricao = request.Descricao,
                Ponto = entityPonto,
                TempoFinal = request.TempoFinal,
                TempoInicio = request.TempoInicio,
                Transitividade = request.Transitividade,
                Distrito = entityDistrito
            };

            try
            {
                _context.Alerta.Add(entityAlertas);

                _context.SaveChanges();

                return new Response<string>(data: entityAlertas.Id.ToString());
            }
            catch
            {
                return new Response<string>(message: $"error while creating: ${ entityAlertas.Id }");
            }
        }
    }
}
