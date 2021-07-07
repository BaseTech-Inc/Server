﻿using Application.Common.Models;
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
    public class PaisesController : ApiControllerBase
    {
        public PaisesController()
        { }

        // GET: api/Paises/
        [HttpGet]
        public async Task<ActionResult<Response<IList<Estado>>>> Create(
            [FromServices] IGetAllPaisesQueryHandler handler,
            [FromQuery] GetAllPaisesQuery command
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
