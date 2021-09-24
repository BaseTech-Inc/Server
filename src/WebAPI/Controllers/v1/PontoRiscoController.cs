using Application.Common.Models;
using Application.PontosRisco.Commands.CreatePontoRisco;
using Application.PontosRisco.Commands.DeletePontoRisco;
using Application.PontosRisco.Commands.UpdatePontoRisco;
using Application.PontosRisco.Queries.GetAllPontoRisco;
using Application.PontosRisco.Queries.GetPontoRiscoById;
using Application.PontosRisco.Queries.GetPontoRiscoWithPagination;
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
    public class PontoRiscoController : ApiControllerBase
    {
        public PontoRiscoController()
        { }

        // GET: api/v1/PontoRisco/
        [HttpGet]
        public async Task<ActionResult<Response<IList<PontoRisco>>>> Get(
            [FromServices] IGetAllPontoRiscoQueryHandler handler,
            [FromQuery] GetAllPontoRiscoQuery command
        )
        {
            var response = handler.Handle(command);

            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Ok(
                response
                );
        }

        // GET: api/v1/PontoRisco/id/:Id
        [HttpGet("id/{Id}")]
        public async Task<ActionResult<Response<PontoRisco>>> GetById(
            [FromServices] IGetPontoRiscoByIdQueryHandler handler,
            [FromRoute] GetPontoRiscoByIdQuery command
        )
        {
            var response = handler.Handle(command);

            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Ok(
                response
                );
        }

        // GET: api/v1/PontoRisco/pagination/
        [HttpGet("pagination/")]
        public async Task<ActionResult<Response<PaginatedList<PontoRisco>>>> GetWithPagination(
            [FromServices] IGetPontoRiscoWithPaginationQueryHandler handler,
            [FromQuery] GetPontoRiscoWithPaginationQuery command
        )
        {
            var response = await handler.Handle(command);

            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Ok(
                response
                );
        }

        // POST: api/v1/PontoRisco/?
        [HttpPost]
        [Authorize(Policy = "ElevatedRights")]
        public async Task<ActionResult<Response<string>>> Create(
            [FromServices] ICreatePontoRiscoCommandHandler handler,
            [FromQuery] CreatePontoRiscoCommand command
        )
        {
            var response = await handler.Handle(command);

            if (!response.Succeeded)
            {
                return BadRequest(response);
            }

            return Created(
                HttpRequestHeader.Referer.ToString(),
                response
                );
        }

        // DELETE: api/v1/PontoRisco/?Id=Id
        [HttpDelete]
        [Authorize(Policy = "ElevatedRights")]
        public async Task<ActionResult<Response<string>>> Delete(
            [FromServices] IDeletePontoRiscoCommandHandler handler,
            [FromQuery] DeletePontoRiscoCommand command
        )
        {
            var response = await handler.Handle(command);

            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Ok(
                response
                );
        }

        // UPDATE: api/PontoRisco/?
        [HttpPut]
        [Authorize(Policy = "ElevatedRights")]
        public async Task<ActionResult<Response<string>>> Update(
            [FromServices] IUpdatePontoRiscoCommandHandler handler,
            [FromQuery] UpdatePontoRiscoCommand command
        )
        {
            var response = await handler.Handle(command);

            if (!response.Succeeded)
            {
                return NotFound(response);
            }

            return Created(
                HttpRequestHeader.Referer.ToString(),
                response
                );
        }
    }
}
