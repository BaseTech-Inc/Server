using Application.Common.Models;
using Application.Distritos.Queries.GetAllDistritos;
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

        // GET: api/Cidades/
        [HttpGet]
        public async Task<ActionResult<Response<IList<Distrito>>>> Create(
            [FromServices] IGetAllDistritosQueryHandler handler,
            [FromQuery] GetAllDistritosQuery command
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
