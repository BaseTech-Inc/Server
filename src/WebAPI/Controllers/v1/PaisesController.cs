using Application.Common.Models;
using Application.Estados.Queries.GetAllEstados;
using Application.Paises.Queries.GetAllPaises;
using Application.Paises.Queries.GetMeshesPaisesById;
using Application.Paises.Queries.GetPaisesById;
using Application.Paises.Queries.GetPaisesByName;
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
    public class PaisesController : ApiControllerBase
    {
        public PaisesController()
        { }

        // GET: api/v1/Paises/
        [HttpGet]
        public async Task<ActionResult<Response<IList<Pais>>>> Get(
            [FromServices] IGetAllPaisesQueryHandler handler,
            [FromQuery] GetAllPaisesQuery command
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

        // GET: api/v1/Paises/name/:name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Response<IList<Pais>>>> GetByName(
            [FromServices] IGetPaisesByNameQueryHandler handler,
            [FromRoute] GetPaisesByNameQuery command
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

        // GET: api/v1/Paises/id/:Id
        [HttpGet("id/{Id}")]
        public async Task<ActionResult<Response<Pais>>> GetById(
            [FromServices] IGetPaisesByIdQueryHandler handler,
            [FromRoute] GetPaisesByIdQuery command
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

        // GET: api/v1/Paises/meshes/:Id
        [HttpGet("meshes/{Id}")]
        public async Task<ActionResult<Response<PaisesVm>>> GetMeshesById(
            [FromServices] IGetMeshesPaisesByIdQueryHandler handler,
            [FromRoute] GetMeshesPaisesByIdQuery command
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
