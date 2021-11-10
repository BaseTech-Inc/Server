using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.PontosRisco.Commands.CreatePontoRisco
{
    public class CreatePontoRiscoCommand
    {
        public string Descricao { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Pais { get; set; }

        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Distrito { get; set; }
    }

    public class CreatePontoRiscoCommandHandler : ICreatePontoRiscoCommandHandler
    {
        private readonly IApplicationDbContext _context;

        public CreatePontoRiscoCommandHandler(
            IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Response<string>> Handle(CreatePontoRiscoCommand request)
        {
            var entityDistrito = _context.Distrito
                .Where(x => EF.Functions.Like(x.Nome, "%" + request.Distrito + "%"))
                    .Where(x => EF.Functions.Like(x.Cidade.Nome, "%" + request.Cidade + "%"))
                        .Where(x => EF.Functions.Like(x.Cidade.Estado.Nome, "%" + request.Estado + "%"))
                            .Where(x => EF.Functions.Like(x.Cidade.Estado.Pais.Nome, "%" + request.Pais + "%"))
                                .Include(e => e.Cidade)
                                    .Include(e => e.Cidade.Estado)
                                        .Include(e => e.Cidade.Estado.Pais)
                                            .OrderByDescending(o => o.Nome)
                                                .ToList()
                                                    .FirstOrDefault();

            var entityPonto = new Ponto
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };

            var entityPontoRisco = new PontoRisco
            {
                Descricao = request.Descricao,
                Ponto = entityPonto,
                Distrito = entityDistrito
            };

            try
            {
                _context.PontoRisco.Add(entityPontoRisco);

                _context.SaveChanges();

                return new Response<string>(data: entityPontoRisco.Id.ToString());
            }
            catch
            {
                return new Response<string>(message: $"erro para criar: ${ entityPontoRisco.Id }");

            }
        }   
    }
}
