using Google.Apis.Auth;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using Domain.Entities;
using Domain.Enumerations;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
    public class GoogleService : IGoogleService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public GoogleService(
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration,
            IApplicationDbContext context,
            ITokenService tokenService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<Response<LoginResponse>> AuthenticateGoogleAsync(string idToken, HttpContext httpContext)
        {
            var googleAuth = _configuration["Authentication:Google:ClientId"];

            GoogleJsonWebSignature.ValidationSettings settings = new GoogleJsonWebSignature.ValidationSettings();

            settings.Audience = new List<string>() { googleAuth };

            GoogleJsonWebSignature.Payload payload = GoogleJsonWebSignature.ValidateAsync(idToken, settings).Result;

            if (payload != null)
            {
                var userExist = await _userManager.FindByNameAsync(payload.Name.ToString());
                var emailExist = await _userManager.FindByEmailAsync(payload.Email.ToString());

                // verifica se não existe nenhum usuário cadastrado com esse username e email
                if ((userExist == null) && (emailExist == null))
                {
                    var usuarioExist = _context.Usuario
                        .Where(x => x.Email == payload.Email)
                            .FirstOrDefault();

                    if (usuarioExist == null)
                    {
                        var usuario = new Usuario
                        {
                            Email = payload.Email,
                            TipoUsuario = new TipoUsuario
                            {
                                Descricao = EnumTipoUsuario.Comum
                            },
                        };

                        _context.Usuario.Add(usuario);
                        _context.SaveChanges();

                        usuarioExist = usuario;
                    }

                    var token = await _tokenService.GenerateTokenJWT(usuarioExist.Id, userName: payload.Name, email: payload.Email);

                    var response = new LoginResponse()
                    {
                        uid = usuarioExist.Id,
                        access_token = token.tokenString,
                        token_type = "bearer",
                        expiration = token.validTo
                    };

                    return new Response<LoginResponse>(response, message: $"Autenticado { payload.Name }");
                }
            }

            return new Response<LoginResponse>(message: $"Ocorreu um erro ao autenticar o usuário.");
        }
    }
}
