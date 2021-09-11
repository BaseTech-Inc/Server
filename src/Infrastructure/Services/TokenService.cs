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
