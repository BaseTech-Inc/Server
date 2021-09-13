using Application.Common.Models;
using Application.Distritos.Queries.GetAllDistritos;
using Application.Distritos.Queries.GetDistritosById;
using Application.Distritos.Queries.GetDistritosByName;
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
    public class DistritosController : ApiControllerBase
    {
        public DistritosController()
        { }

        // GET: api/v1/Distritos/
        [HttpGet]
        public async Task<ActionResult<Response<IList<Distrito>>>> Get(
            [FromServices] IGetAllDistritosQueryHandler handler,
            [FromQuery] GetAllDistritosQuery command
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

        // GET: api/v1/Distritos/name/:name
        [HttpGet("name/{name}")]
        public async Task<ActionResult<Response<IList<Distrito>>>> GetByName(
            [FromServices] IGetDistritosByNameQueryHandler handler,
            [FromRoute] GetDistritosByNameQuery command
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

        // GET: api/v1/Distritos/id/:Id
        [HttpGet("id/{Id}")]
        public async Task<ActionResult<Response<Distrito>>> GetById(
            [FromServices] IGetDistritosByIdQueryHandler handler,
            [FromRoute] GetDistritosByIdQuery command
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
