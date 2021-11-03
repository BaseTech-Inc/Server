using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

        public string Pais { get; set; }

        public string Estado { get; set; }

        public string Cidade { get; set; }

        public string Distrito { get; set; }
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

                    entity.Descricao = request.Descricao;

                    entity.Ponto = new Ponto()
                    {
                        Latitude = request.Latitude,
                        Longitude = request.Longitude
                    };

                    entity.Distrito = entityDistrito;

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
