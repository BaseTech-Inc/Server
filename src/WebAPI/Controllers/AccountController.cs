using Application.Common.Interfaces;
using Application.Common.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly IIdentityService _identityService;

        public AccountController(
            IIdentityService identityService)
        {
            _identityService = identityService;
        }        

        // POST: api/account/login/
        [HttpPost("login")]
        public async Task<ActionResult<Response<string>>> Login(string email, string password)
        {
            var authorizeResult = await _identityService.AuthenticateAsync(email, password);

            if (!authorizeResult.Succeeded)
            {
                return Unauthorized();
            }

            return Ok(authorizeResult);
        }

        // POST: api/account/register/
        [HttpPost("register")]
        public async Task<ActionResult<Response<string>>> Register(string username, string email, string password, int age, string phone)
        {
            var createUserResult = await _identityService.RegisterAsync(username, email, password, age, phone);

            if (!createUserResult.Succeeded)
            {
                return BadRequest();
            }

            return Ok(createUserResult);
        }
    }
}
