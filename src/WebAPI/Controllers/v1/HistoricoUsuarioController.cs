using Application.Common.Models;
using Application.HistoricoUsuarios.Commands.CreateHistorico;
using Application.HistoricoUsuarios.Commands.DeleteHistorico;
using Application.HistoricoUsuarios.Queries.GetAllHistorico;
using Application.HistoricoUsuarios.Queries.GetHistoricoByDate;
using Application.HistoricoUsuarios.Queries.GetHistoricoById;
using Application.HistoricoUsuarios.Queries.GetHistoricoByName;
using Application.HistoricoUsuarios.Queries.GetHistoricoByNameWithPagination;
using Application.HistoricoUsuarios.Queries.GetHistoricoWithPagination;
using Application.HistoricoUsuarios.Queries.GetMaisPesquisadosWithPagination;
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
        /// <summary>
        /// Não é para passar o userId
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<Response<IList<HistoricoUsuario>>>> Get(
            [FromServices] IGetAllHistoricoQueryHandler handler,
            [FromQuery] GetAllHistoricoQuery command
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

        // GET: api/v1/HistoricoUsuario/id/
        /// <summary>
        /// Não é para passar o userId
        /// </summary>
        [HttpGet("id/")]
        public async Task<ActionResult<Response<HistoricoUsuario>>> GetById(
            [FromServices] IGetHistoricoByIdQueryHandler handler,
            [FromQuery] GetHistoricoByIdQuery command
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

        // GET: api/v1/HistoricoUsuario/id/
        /// <summary>
        /// Não é para passar o userId
        /// </summary>
        [HttpGet("date/")]
        public async Task<ActionResult<Response<IList<HistoricoUsuario>>>> GetByDate(
            [FromServices] IGetHistoricoByDateQueryHandler handler,
            [FromQuery] GetHistoricoByDateQuery command
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

        // GET: api/v1/HistoricoUsuario/name/
        [HttpGet("name/")]
        public async Task<ActionResult<Response<IList<HistoricoUsuario>>>> GetByName(
            [FromServices] IGetHistoricoByNameQueryHandler handler,
            [FromQuery] GetHistoricoByNameQuery command
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

        // GET: api/v1/HistoricoUsuario/pagination/
        [HttpGet("pagination/")]
        public async Task<ActionResult<Response<PaginatedList<HistoricoUsuario>>>> GetWithPagination(
            [FromServices] IGetHistoricoWithPaginationQueryHandler handler,
            [FromQuery] GetHistoricoWithPaginationQuery command
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

        // GET: api/v1/HistoricoUsuario/name/pagination/
        [HttpGet("name/pagination/")]
        public async Task<ActionResult<Response<PaginatedList<HistoricoUsuario>>>> GetByNameWithPagination(
            [FromServices] IGetHistoricoByNameWithPaginationQueryHandler handler,
            [FromQuery] GetHistoricoByNameWithPaginationQuery command
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

        // POST: api/v1/HistoricoUsuario/?
        /// <summary>
        /// Não é para passar o userId
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Response<string>>> Create(
            [FromServices] ICreateHistoricoCommandHandler handler,
            [FromQuery] CreateHistoricoCommand command
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

        // DELETE: api/v1/Marcadores/?Id=Id&UserId=UserId
        /// <summary>
        /// Não é para passar o userId
        /// </summary>
        [HttpDelete]
        public async Task<ActionResult<Response<string>>> Delete(
            [FromServices] IDeleteHistoricoCommandHandler handler,
            [FromQuery] DeleteHistoricoCommand command
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

        // GET: api/v1/HistoricoUsuario/more-search
        /// <summary>
        /// Não é para passar o userId
        /// </summary>
        [HttpGet("more-search/")]
        public async Task<ActionResult<Response<PaginatedList<MaisPesquisadosDto>>>> GetMoreSearch(
            [FromServices] IGetMaisPesquisadosWithPaginationQueryHandler handler,
            [FromQuery] GetMaisPesquisadosWithPaginationQuery command
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
    }
}
