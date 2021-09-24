﻿using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
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
            var entityPonto = new Ponto
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude
            };

            var entityPontoRisco = new PontoRisco
            {
                Descricao = request.Descricao,
                Ponto = entityPonto
            };

            try
            {
                _context.PontoRisco.Add(entityPontoRisco);

                _context.SaveChanges();

                return new Response<string>(data: entityPontoRisco.Id.ToString());
            }
            catch
            {
                return new Response<string>(message: $"error while creating: ${ entityPontoRisco.Id }");
            }

            return null;
        }
    }
}
