using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using Application.Common.Enumerations;
using Domain.Entities;
using Domain.Enumerations;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IApplicationDbContext _context;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            RoleManager<IdentityRole> roleManager,
            IApplicationDbContext context)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<Response<LoginResponse>> AuthenticateAsync(string email, string password)
        {
            // verifica se o usuário existe, para não gerar futuros erros
            var user = await _userManager.FindByEmailAsync(email);

            if (!(user == null))
            {
                // verifica se a senha passada é correta
                var checkPassword = await _userManager.CheckPasswordAsync(user, password);

                if (checkPassword)
                {
                    if (user.EmailConfirmed)
                    {
                        var authenticatedRole = new IdentityRole(Roles.Authenticated.ToString());

                        if (_roleManager.Roles.All(r => r.Name != authenticatedRole.Name))
                        {
                            await _roleManager.CreateAsync(authenticatedRole);
                        }

                        await _userManager.AddToRolesAsync(user, new[] { authenticatedRole.Name });

                        var usuario = _context.Usuario.Where(x => x.ApplicationUserID == user.Id).FirstOrDefault();

                        var token = await _tokenService.GenerateTokenJWT(user.Id, usuario.Id);

                        var response = new LoginResponse()
                        {
                            uid = usuario.Id,
                            access_token = token.tokenString,
                            token_type = "bearer",
                            expiration = token.validTo
                        };

                        return new Response<LoginResponse>(response, message: $"Authenticated { user.UserName }");
                    }
                }
            }

            return new Response<LoginResponse>(message: $"An error occurred while authenticating user.");
        }

        public async Task<Response<string>> RegisterAsync(string username, string email, string password)
        {
            var userExist = await _userManager.FindByNameAsync(username);
            var emailExist = await _userManager.FindByEmailAsync(email);

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

                listUsuario.Add(usuario);                

                var appUser = new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                    Usuario = listUsuario
                };

                var resultCreateAppUser = await _userManager.CreateAsync(appUser, password);

                if (resultCreateAppUser.Succeeded)
                {
                    var tokenEmail = await _tokenService.GenerateTokenEmail(appUser.Id);

                    return new Response<string>(usuario.Id, message: $"User Registered. Please confirm your account by visiting this URL { tokenEmail }");
                }
            } 
            else
            {
                return new Response<string>(message: $"You already have a registered user with this credential.");
            }

            return new Response<string>(message: $"Error during registration.");
        }

        public async Task<Response<string>> VerifyEmailAsync(string userId, string tokenEmail)
        {
            var usuario = _context.Usuario.Where(x => x.Id == userId).FirstOrDefault();

            var user = await _userManager.FindByIdAsync(usuario.ApplicationUserID);

            if (user != null)
            {
                var resultConfirmEmail = await _userManager.ConfirmEmailAsync(user, tokenEmail);

                if (resultConfirmEmail.Succeeded)
                {
                    return new Response<string>(usuario.Id, message: $"Email confirmed successfully.");
                }
            }

            return new Response<string>(message: $"Failed to verify email.");
        }
    }
}
