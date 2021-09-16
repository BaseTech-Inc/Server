using Application.Cidades.Queries.GetAllCidades;
using Application.Cidades.Queries.GetCidadesById;
using Application.Cidades.Queries.GetCidadesByName;
using Application.Cidades.Queries.GetMeshesCidadeById;
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
    public class CidadesController : ApiControllerBase
    {
        public CidadesController()
        { }

        // GET: api/v1/Cidades/
        [HttpGet]
        public async Task<ActionResult<Response<IList<Cidade>>>> Get(
            [FromServices] IGetAllCidadeQueryHandler handler,
            [FromQuery] GetAllCidadeQuery command
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

        // GET: api/v1/Cidades/name/:name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Response<IList<Cidade>>>> GetByName(
            [FromServices] IGetCidadesByNameQueryHandler handler,
            [FromRoute] GetCidadesByNameQuery command
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

        // GET: api/v1/Cidades/id/:Id
        [HttpGet("id/{Id}")]
        public async Task<ActionResult<Response<Cidade>>> GetById(
            [FromServices] IGetCidadesByIdQueryHandler handler,
            [FromRoute] GetCidadesByIdQuery command
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

        // GET: api/v1/Cidades/meshes/:Id
        [HttpGet("meshes/{Id}")]
        public async Task<ActionResult<Response<CidadeVm>>> GetMeshesById(
            [FromServices] IGetMeshesCidadesByIdQueryHandler handler,
            [FromRoute] GetMeshesCidadesByIdQuery command
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
