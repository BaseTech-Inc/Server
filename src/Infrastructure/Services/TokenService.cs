using Application.Common.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenService(
            IConfiguration configuration,
            UserManager<ApplicationUser> userManager)
        {
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<(string tokenString, DateTime validTo)> GenerateTokenJWT(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var claims = new List<Claim>();

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserName));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim("uid", user.Id.ToString()));

            foreach (var role in await _userManager.GetRolesAsync(user))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var secretBytes = Encoding.UTF8.GetBytes("p3s6v9yBEHMbQeThWmZq4t7wzC");
            var key = new SymmetricSecurityKey(secretBytes);
            var algorithm = SecurityAlgorithms.HmacSha256;

            var signingCredentials = new SigningCredentials(key, algorithm);

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                claims,
                notBefore: DateTime.Now,
                expires: DateTime.Now.AddHours(1),
                signingCredentials);

            return (new JwtSecurityTokenHandler().WriteToken(token).ToString(), token.ValidTo);
        }

        public string DecodeTokenJwt(string part)
        {
            var bytes = Convert.FromBase64String(part);
            var stringBytes = Encoding.UTF8.GetString(bytes);

            return stringBytes;
        }

        public async Task<string> GenerateTokenEmail(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var tokenEmailConfirmation = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            return tokenEmailConfirmation;
        }
    }
}
