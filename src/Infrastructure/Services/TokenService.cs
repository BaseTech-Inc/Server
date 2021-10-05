using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Application.Common.Interfaces;
using Infrastructure.Identity;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationDbContext _context;

        public TokenService(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager,
            IApplicationDbContext context)
        {
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
        }

        public async Task<(string tokenString, DateTime validTo)> GenerateTokens(Usuario usuario, string userId, HttpContext httpContext)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);

            // Generate access token
            var accessToken = await GenerateTokenJWT(usuario.Id, identityUser.Id, userName: identityUser.UserName, email: identityUser.Email);

            // Generate refresh token and set it to cookie
            var ipAddress = httpContext.Connection.RemoteIpAddress.ToString();
            var refreshToken = GenerateRefreshToken(ipAddress, identityUser.Id);

            // Set Refresh Token Cookie
            var cookieOptions = new CookieOptions
            {
                HttpOnly = false,
                Expires = DateTime.UtcNow.AddDays(30)
            };

            httpContext.Response.Cookies.Append("refreshToken", refreshToken.Token, cookieOptions);

            // Save refresh token to database
            if (identityUser.RefreshTokens == null)
            {
                identityUser.RefreshTokens = new List<RefreshToken>();
            }

            identityUser.RefreshTokens.Add(refreshToken);
            _context.SaveChanges();
            return accessToken;
        }

        public async Task<(string tokenString, DateTime validTo)> GenerateTokenJWT(string usuarioId, string userId = null, string userName = null, string email = null)
        {
            string _userName;
            string _email;

            var claims = new List<Claim>();

            if (userId != null)
            {
                var user = await _userManager.FindByIdAsync(userId);

                _userName = user.UserName;
                _email = user.Email;

                foreach (var role in await _userManager.GetRolesAsync(user))
                {
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
            }
            else
            {
                _userName = userName;
                _email = email;
            }

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, _userName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, _email));
            claims.Add(new Claim("uid", usuarioId));

            var secretBytes = Encoding.UTF8.GetBytes(_configuration["JWT:SecretKeyy"]);
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddDays(1),
                signingCredentials);

            return (new JwtSecurityTokenHandler().WriteToken(token).ToString(), token.ValidTo);
        }

        public string DecodeTokenJwt(string part)
        {
            var bytes = Convert.FromBase64String(part);
            var stringBytes = Encoding.UTF8.GetString(bytes);

            return stringBytes;
        }

        private RefreshToken GenerateRefreshToken(string ipAddress, string userId)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);

                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    ExpiryOn = DateTime.UtcNow.AddDays(30),
                    CreatedOn = DateTime.UtcNow,
                    CreatedByIp = ipAddress,
                    UserId = userId
                };
            }
        }

        public async Task<bool> RevokeRefreshToken(HttpContext httpContext, string token = null)
        {
            token = token == null ? httpContext.Request.Cookies["refreshToken"] : token;
            var identityUserTask = _userManager.Users;

            var identityUser = await identityUserTask.Include(x => x.RefreshTokens).FirstOrDefaultAsync(x => x.RefreshTokens.Any(y => y.Token == token && y.UserId == x.Id));

            if (identityUser == null)
            {
                return false;
            }

            // Revoke Refresh token
            var existingToken = identityUser.RefreshTokens.FirstOrDefault(x => x.Token == token);
            existingToken.RevokedByIp = httpContext.Connection.RemoteIpAddress.ToString();
            existingToken.RevokedOn = DateTime.UtcNow;
            _context.SaveChanges();

            return true;
        }

        public async Task<string> GenerateTokenEmail(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var tokenEmailConfirmation = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return tokenEmailConfirmation;
        }
    }
}
