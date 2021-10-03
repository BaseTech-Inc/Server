using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public readonly IIdentityService _identityService;
        private readonly IGoogleService _googleService;

        public AccountController(
            IIdentityService identityService,
            IGoogleService googleService)
        {
            _identityService = identityService;
            _googleService = googleService;
        }

        // POST: api/account/login/?email=email&password=password
        [HttpPost("login")]
        public async Task<ActionResult<Response<LoginResponse>>> Login(string email, string password)
        {
            var authorizeResult = await _identityService.AuthenticateAsync(email, password, HttpContext);

            if (!authorizeResult.Succeeded)
            {
                return BadRequest(authorizeResult);
            }

            return Ok(authorizeResult);
        }

        // POST: api/account/login-google/?idToken=idToken
        [HttpPost("login-google")]
        public async Task<ActionResult<Response<LoginResponse>>> LoginGoogle(string idToken)
        {
            var authorizeResult = await _googleService.AuthenticateGoogleAsync(idToken, HttpContext);

            if (!authorizeResult.Succeeded)
            {
                return BadRequest(authorizeResult);
            }

            return Ok(authorizeResult);
        }

        // POST: api/account/register/?username=username&email=email&password=password
        [HttpPost("register")]
        public async Task<ActionResult<Response<string>>> Register(string username, string email, string password)
        {
            var createUserResult = await _identityService.RegisterAsync(username, email, password);

            if (!createUserResult.Succeeded)
            {
                return BadRequest(createUserResult);
            }

            return Ok(createUserResult);
        }

        // POST: api/account/refresh-token/?
        [HttpPost("refresh-token")]
        public async Task<ActionResult<Response<LoginResponse>>> RefreshToken()
        {
            var verifyRefreshTokenResult = await _identityService.RefreshToken(HttpContext);

            if (!verifyRefreshTokenResult.Succeeded)
            {
                return BadRequest(verifyRefreshTokenResult);
            }

            return Ok(verifyRefreshTokenResult);
        }

        // POST: api/account/revoke-token/?
        [HttpPost("revoke-token")]
        public async Task<ActionResult<Response<string>>> RevokeToken(string token)
        {
            var verifyRevokeTokenResult = await _identityService.RevokeToken(HttpContext, token);

            if (!verifyRevokeTokenResult.Succeeded)
            {
                return BadRequest(verifyRevokeTokenResult);
            }

            return Ok(verifyRevokeTokenResult);
        }

        // POST: api/account/logout/?
        [HttpPost("logout")]
        public async Task<ActionResult<Response<string>>> Logout()
        {
            var verifyLogoutResult = await _identityService.LogoutAsync(HttpContext);

            if (!verifyLogoutResult.Succeeded)
            {
                return BadRequest(verifyLogoutResult);
            }

            return Ok(verifyLogoutResult);
        }

        // POST: api/account/verify-email/?userId=userId&tokenEmail=tokenEmail
        [HttpPost("verify-email")]
        public async Task<ActionResult<Response<string>>> VerifyEmail(string userId, string tokenEmail)
        {
            var verifyEmailResult = await _identityService.VerifyEmailAsync(userId, tokenEmail);

            if (!verifyEmailResult.Succeeded)
            {
                return BadRequest(verifyEmailResult);
            }

            return Ok(verifyEmailResult);
        }

        // POST: api/account/generate-password-reset/?
        [HttpPost("generate-password-reset")]
        public async Task<ActionResult<Response<string>>> GeneretePasswordReset(string email)
        {
            var generetePasswordResetResult = await _identityService.GeneretPasswordResetAsync(email);

            if (!generetePasswordResetResult.Succeeded)
            {
                return BadRequest(generetePasswordResetResult);
            }

            return Ok(generetePasswordResetResult);
        }

        // POST: api/account/change-password/?
        [HttpPost("change-password")]
        public async Task<ActionResult<Response<string>>> ChangePassword(string email, string token, string password)
        {
            var changePasswordResult = await _identityService.ChangePasswordAsync(email, token, password);

            if (!changePasswordResult.Succeeded)
            {
                return BadRequest(changePasswordResult);
            }

            return Ok(changePasswordResult);
        }

        // POST: api/account/change-password/id
        [HttpPost("change-password/id")]
        [Authorize]
        public async Task<ActionResult<Response<string>>> ChangePasswordWithId(string UserId, string oldPassword, string newPassword)
        {
            var changePasswordResult = await _identityService.ChangePasswordWithIdAsync(UserId, oldPassword, newPassword);

            if (!changePasswordResult.Succeeded)
            {
                return BadRequest(changePasswordResult);
            }

            return Ok(changePasswordResult);
        }

        // GET: api/account/get-basic-profile
        [HttpGet("basic-profile")]
        [Authorize]
        public async Task<ActionResult<Response<IDictionary<string, string>>>> GetBasicProfile(string UserId)
        {
            var getBasicProfile = await _identityService.GetBasicProfile(UserId);

            if (!getBasicProfile.Succeeded)
            {
                return BadRequest(getBasicProfile);
            }

            return Ok(getBasicProfile);
        }

        // PUT: api/account/get-basic-profile
        [HttpPut("basic-profile")]
        [Authorize]
        public async Task<ActionResult<Response<IDictionary<string, string>>>> UpdateBasicProfile(
            string UserId, 
            string UserName, 
            string TipoUsuario)
        {
            var updateBasicProfile = await _identityService.UpdateBasicProfile(UserId, UserName, TipoUsuario);

            if (!updateBasicProfile.Succeeded)
            {
                return BadRequest(updateBasicProfile);
            }

            return Ok(updateBasicProfile);
        }

        // DELETE: api/account/
        [HttpDelete]
        [Authorize]
        public async Task<ActionResult<Response<string>>> Delete(
            string UserId)
        {
            var deleteAccount = await _identityService.DeleteAsync(UserId);

            if (!deleteAccount.Succeeded)
            {
                return BadRequest(deleteAccount);
            }

            return Ok(deleteAccount);
        }
    }
}
