using Application.Common.Models;
using Application.Estados.Queries.GetAllEstados;
using Application.Estados.Queries.GetEstadosById;
using Application.Estados.Queries.GetEstadosByName;
using Application.Estados.Queries.GetMeshesEstadosById;
using Application.Estados.Queries.GetPaisesByName;
using Application.Paises.Queries.GetAllPaises;
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
    public class EstadosController : ApiControllerBase
    {
        public EstadosController()
        { }

        // GET: api/v1/Estados/
        [HttpGet]
        public async Task<ActionResult<Response<IList<Estado>>>> Get(
            [FromServices] IGetAllEstadosQueryHandler handler,
            [FromQuery] GetAllEstadosQuery command
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

        // GET: api/v1/Estados/name/:name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Response<IList<Estado>>>> GetByName(
            [FromServices] IGetEstadosByNameQueryHandler handler,
            [FromRoute] GetEstadosByNameQuery command
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

        // GET: api/v1/Estados/id/:Id
        [HttpGet("id/{Id}")]
        public async Task<ActionResult<Response<Estado>>> GetById(
            [FromServices] IGetEstadosByIdQueryHandler handler,
            [FromRoute] GetEstadosByIdQuery command
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

        // GET: api/v1/Estados/meshes/:Id
        [HttpGet("meshes/{Id}")]
        public async Task<ActionResult<Response<EstadoVm>>> GetMeshesById(
            [FromServices] IGetMeshesEstadoByIdQueryHandler handler,
            [FromRoute] GetMeshesEstadoByIdQuery command
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
    }
}
