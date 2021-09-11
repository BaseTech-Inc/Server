using Application.Common.Models;
using Application.Marcador.Commands.CreateMarcadores;
using Application.Marcador.Commands.DeleteMarcadores;
using Application.Marcador.Commands.UpdateMarcadores;
using Application.Marcador.Queries.GetAllMarcadores;
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
    public class MarcadoresController : ApiControllerBase
    {
        public MarcadoresController()
        { }

        // GET: api/v1/Marcadores/
        /// <summary>
        /// Não é para passar o userId
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<Response<IList<Marcadores>>>> Get(
            [FromServices] IGetAllMarcadoresQueryHandler handler,
            [FromQuery] GetAllMarcadoresQuery command
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

        // POST: api/v1/Marcadores/?latitude=latitude&longitude=longitude&Nome=Nome
        /// <summary>
        /// Não é para passar o userId
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Response<string>>> Create(
            [FromServices] ICreateMarcadoresCommandHandler handler,
            [FromQuery] CreateMarcadoresCommand command
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

        // DELETE: api/v1/Marcadores/?Id=Id
        /// <summary>
        /// Não é para passar o userId
        /// </summary>
        [HttpDelete]
        public async Task<ActionResult<Response<string>>> Delete(
            [FromServices] IDeleteMarcadoresCommandHandler handler,
            [FromQuery] DeleteMarcadoresCommand command
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

        // UPDATE: api/Marcadores/?Id=Id&latitude=latitude&longitude=longitude&Nome=Nome
        /// <summary>
        /// Não é para passar o userId
        /// </summary>
        [HttpPut]
        public async Task<ActionResult<Response<string>>> Update(
            [FromServices] IUpdateMarcadoresCommandHandler handler,
            [FromQuery] UpdateMarcadoresCommand command
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
    }
}
