using Application.Cidades.Queries.GetAllCidades;
using Application.Cidades.Queries.GetCidadesByName;
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

        // GET: api/Cidades/
        [HttpGet]
        public async Task<ActionResult<Response<IList<Cidade>>>> Create(
            [FromServices] IGetAllCidadeQueryHandler handler,
            [FromQuery] GetAllCidadeQuery command
        )
        {
            var response = handler.Handle(command);

            if (!response.Succeeded)
            {
                return NotFound();
            }

            return Created(
                HttpRequestHeader.Referer.ToString(),
                response
                );
        }

        // GET: api/Cidades/:name
        [HttpGet("{name}")]
        public async Task<ActionResult<Response<IList<Cidade>>>> CreateByName(
            [FromServices] IGetCidadesByNameQueryHandler handler,
            [FromRoute] GetCidadesByNameQuery command
        )
        {
            var response = handler.Handle(command);

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
