using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Application.Common.Interfaces;
using Application.Common.Models;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public IdentityService(
            UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<string>> AuthenticateAsync(string email, string password)
        {
            // verifica se o usuário existe, para não gerar futuros erros
            var user = await _userManager.FindByEmailAsync(email);

            if (!(user == null))
            {
                // verifica se a senha passada é correta
                var checkPassword = await _userManager.CheckPasswordAsync(user, password);

                if (checkPassword)
                {
                    return new Response<string>("", message: $"Authenticated { user.UserName }");
                }
            }

            return new Response<string>(message: $"An error occurred while authenticating user.");
        }

        public async Task<Response<string>> RegisterAsync(string username, string email, string password)
        {
            var userExist = await _userManager.FindByNameAsync(username);
            var emailExist = await _userManager.FindByEmailAsync(email);

            // verifica se não existe nenhum usuário cadastrado com esse username e email
            if ((userExist == null) && (emailExist == null))
            {
                // cria o objeto do usuário
                var user = new ApplicationUser
                {
                    UserName = username,
                    Email = email
                };

                // cria o usuário no contexto
                var resultCreate = await _userManager.CreateAsync(user, password);

                if (resultCreate.Succeeded)
                {
                    var tokenEmailConfirmation = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    var resultConfirmEmail = await _userManager.ConfirmEmailAsync(user, tokenEmailConfirmation);

                    if (resultConfirmEmail.Succeeded)
                    {
                        return new Response<string>(user.Id, message: $"User Registered");
                    }                    
                }
            } else
            {
                return new Response<string>(message: $"You already have a registered user with this credential.");
            }

            return new Response<string>(message: $"Error during registration.");
        }
    }
}
