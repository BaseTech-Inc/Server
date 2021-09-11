using Application.Common.Models;
using Application.HistoricoUsuarios.Commands.CreateHistorico;
using Application.HistoricoUsuarios.Commands.DeleteHistorico;
using Application.HistoricoUsuarios.Queries.GetAllHistorico;
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
    public class HistoricoUsuarioController : ApiControllerBase
    {
        public HistoricoUsuarioController()
        { }

        // GET: api/v1/HistoricoUsuario/
        [HttpGet]
        public async Task<ActionResult<Response<IList<HistoricoUsuario>>>> Get(
            [FromServices] IGetAllHistoricoQueryHandler handler,
            [FromQuery] GetAllHistoricoQuery command
        )
        {
            var response = handler.Handle(command);

            if (!response.Succeeded)
            {
                return NotFound();
            }

            return Ok(
                response
                );
        }

        // POST: api/v1/HistoricoUsuario/?userId=userId&LatitudeChegada=LatitudeChegada&LongitudeChegada=LongitudeChegada&...
        [HttpPost]
        public async Task<ActionResult<Response<string>>> Create(
            [FromServices] ICreateHistoricoCommandHandler handler,
            [FromQuery] CreateHistoricoCommand command
        )
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

        // DELETE: api/v1/Marcadores/?Id=Id&UserId=UserId
        [HttpDelete]
        public async Task<ActionResult<Response<string>>> Delete(
            [FromServices] IDeleteHistoricoCommandHandler handler,
            [FromQuery] DeleteHistoricoCommand command
        )
        {
            var response = await handler.Handle(command);

            if (!response.Succeeded)
            {
                return NotFound();
            }

            return Ok(
                response
                );
        }
    }
}
