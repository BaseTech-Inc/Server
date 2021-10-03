using Application.Common.Models;
using Application.Localidades.Queries.GetLocalidadesByNames;
using Domain.Entities;
using Infrastructure.Flooding;
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
    public class LocalidadesController : ApiControllerBase
    {
        public LocalidadesController()
        { }

        // GET: api/v1/localidades/?namePais=namePais&nameEstado=nameEstado&nameCidade=nameCidade&nameDistrito=nameDistrito
        [HttpGet]
        public async Task<ActionResult<Response<IList<Distrito>>>> Get(
            [FromServices] IGetLocalidadeByNameQueryHandler handler,
            [FromQuery] GetLocalidadesByNameQuery command
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
