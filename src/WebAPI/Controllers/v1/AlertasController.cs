using Application.Alertas.Commands.CreateAlertas;
using Application.Common.Interfaces;
using Application.Common.Models;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace WebAPI.Controllers.v1
{
    [Authorize]
    public class AlertasController : ApiControllerBase
    {
        private readonly ICGESPService _CGESPService;

        public AlertasController(
            ICGESPService CGESPService)
        {
            _CGESPService = CGESPService;
        }

        [HttpGet]
        public async Task<ActionResult<Response<IList<Alerta>>>> Get(
            int year,
            int month,
            int day)
        {
            var response = await _CGESPService.ProcessCGESPByDate(new DateTime(year, month, day));

            if (!response.Succeeded)
            {
                return NotFound();
            }

            return Ok(
                response
                );
        }

        [HttpGet("Bairro")]
        public async Task<ActionResult<Response<IList<Alerta>>>> GetByDistrict(
            int year,
            int month,
            int day,
            string district)
        {
            var response = await _CGESPService.ProcessCGESPByDistrict(new DateTime(year, month, day), district);

            if (!response.Succeeded)
            {
                return NotFound();
            }

            return Ok(
                response
                );
        }

        [HttpPost]
        [Authorize(Policy = "ElevatedRights")]
        public async Task<ActionResult<Response<string>>> Create(
            [FromServices] ICreateAlertasCommandHandler handler,
            [FromQuery] CreateAlertasCommand command)
        {
            var response = await handler.Handle(command);

            if (!response.Succeeded)
            {
                return NotFound();
            }

            return Created(
                HttpRequestHeader.Referer.ToString(),
                response
                );
        }
    }
}
