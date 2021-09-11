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

        public async Task<Response<LoginResponse>> AuthenticateGoogleAsync(string idToken)
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
                    var listUsuario = new List<Usuario>();

                    var usuario = new Usuario
                    {
                        TipoUsuario = new TipoUsuario
                        {
                            Descricao = EnumTipoUsuario.Comum
                        }
                    };

                    _context.Usuario.Add(usuario);

                    var token = await _tokenService.GenerateTokenJWT(usuarioId: usuario.Id, userName: payload.Name, email: payload.Email);

                    var response = new LoginResponse()
                    {
                        uid = usuario.Id,
                        access_token = token.tokenString,
                        token_type = "bearer",
                        expiration = token.validTo
                    };

                    return new Response<LoginResponse>(response, message: $"Authenticated { payload.Name }");
                }
            }

            return new Response<LoginResponse>(message: $"An error occurred while authenticating user.");
        }
    }
}
