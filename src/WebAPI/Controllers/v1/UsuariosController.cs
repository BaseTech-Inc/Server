using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers.v1
{
    [Authorize(Policy = "ElevatedRights")]
    public class UsuariosController : ApiControllerBase
    {
        private readonly IIdentityGetService _identityGetService;

        public UsuariosController(
            IIdentityGetService identityGetService) 
        {
            _identityGetService = identityGetService;
        }

        // GET: api/v1/Usuarios
        [HttpGet]
        public async Task<ActionResult<Response<IList<UsuarioResponse>>>> Get()
        {
            var usuariosResult = await _identityGetService.GetAllIdentity();

            if (!usuariosResult.Succeeded)
            {
                return NotFound(usuariosResult);
            }

            return Ok (
                usuariosResult
                );
        }
    }
}
