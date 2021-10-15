using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Web;
using Microsoft.Extensions.Configuration;

using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using Application.Common.Enumerations;
using Domain.Entities;
using Domain.Enumerations;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Text;

namespace Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IApplicationDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            ITokenService tokenService,
            RoleManager<IdentityRole> roleManager,
            IApplicationDbContext context,
            IEmailService emailService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
            _context = context;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<Response<LoginResponse>> AuthenticateAsync(string email, string password, HttpContext httpContext)
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

                        // Generate tokens
                        var token = await _tokenService.GenerateTokens(usuario, user.Id, httpContext);

                        var response = new LoginResponse()
                        {
                            uid = usuario.Id,
                            access_token = token.tokenString,
                            token_type = "bearer",
                            expiration = token.validTo,
                            refresh_token = token.refreshToken
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
                var passwordValidator = new PasswordValidator<ApplicationUser>();
                var result = await passwordValidator.ValidateAsync(_userManager, null, password);

                if (result.Succeeded)
                {
                    var listUsuario = new List<Usuario>();

                    var usuario = new Usuario
                    {
                        Email = email,
                        TipoUsuario = new TipoUsuario
                        {
                            Descricao = EnumTipoUsuario.Comum
                        },
                        Nome = username
                    };

                    listUsuario.Add(usuario);

                    var appUser = new ApplicationUser
                    {
                        UserName = username.Replace(" ", ""),
                        Email = email,
                        Usuario = listUsuario,
                    };

                    var resultCreateAppUser = await _userManager.CreateAsync(appUser, password);

                    if (resultCreateAppUser.Succeeded)
                    {
                        var tokenEmail = await _tokenService.GenerateTokenEmail(appUser.Id);
                        var url = $"https://tupaweb.azurewebsites.net/Login/Verfiy?userId=" + HttpUtility.UrlEncode(usuario.Id) + "&tokenEmail=" + HttpUtility.UrlEncode(tokenEmail);

                        string subject = "Verificar Email";

                        await _emailService.SendEmailAsync(
                            appUser.Email,
                            _emailService.templateEmail(
                                subject,
                                appUser.UserName,
                                "Clique no botão abaixo para confirmar seu endereço de e-mail e ativar sua conta.",
                                "Se você recebeu está mensagem por engano, simplesmente ignore este e-mail e não clique no botão.",
                                url,
                                "Verificar Email!"),
                            subject);

                        return new Response<string>(usuario.Id, message: $"User Registered.");
                    }
                } else
                {
                    return new Response<string>(message: $"Password not valid.");
                }                
            } 
            else
            {
                return new Response<string>(message: $"You already have a registered user with this credential.");
            }

            return new Response<string>(message: $"Error during registration.");
        }

        public async Task<Response<LoginResponse>> RefreshToken(HttpContext httpContext)
        {
            Func<RefreshToken, bool> IsRefreshTokenValid = existingToken =>
            {
                // Is token already revoked, then return false
                if (existingToken.RevokedByIp != null && existingToken.RevokedOn != DateTime.MinValue)
                {
                    return false;
                }

                // Token already expired, then return false
                if (existingToken.ExpiryOn <= DateTime.UtcNow)
                {
                    return false;
                }

                return true;
            };

            var token = httpContext.Request.Cookies["refreshToken"];
            var identityUserTask = _userManager.Users;
            var identityUser = identityUserTask.Include(x => x.RefreshTokens).Include(x => x.Usuario)
                .FirstOrDefault(x => x.RefreshTokens.Any(y => y.Token == token && y.UserId == x.Id));

            // Get existing refresh token if it is valid and revoke it
            var existingRefreshToken = identityUser.RefreshTokens.FirstOrDefault(x => x.Token == token);

            if (!IsRefreshTokenValid(existingRefreshToken))
            {
                return new Response<LoginResponse>(message: $"Failed.");
            }

            existingRefreshToken.RevokedByIp = httpContext.Connection.RemoteIpAddress.ToString();
            existingRefreshToken.RevokedOn = DateTime.UtcNow;

            // Generate new tokens
            var newToken = await _tokenService.GenerateTokens(identityUser.Usuario.FirstOrDefault(), identityUser.Id, httpContext);

            var response = new LoginResponse()
            {
                uid = identityUser.Usuario.FirstOrDefault().Id,
                access_token = newToken.tokenString,
                token_type = "bearer",
                expiration = newToken.validTo,
                refresh_token = newToken.refreshToken
            };

            return new Response<LoginResponse>(response, message: $"Success.");
        }

        public async Task<Response<string>> RevokeToken(HttpContext httpContext, string token)
        {
            var result = await _tokenService.RevokeRefreshToken(httpContext, token);

            // If user found, then revoke
            if (result)
            {
                return new Response<string>("", message: $"Success.");
            }

            // Otherwise, return error
            return new Response<string>(message: $"Failed.");
        }

        public async Task<Response<string>> LogoutAsync(HttpContext httpContext)
        {
            // Revoke Refresh Token 
            await _tokenService.RevokeRefreshToken(httpContext);

            return new Response<string>("", message: $"Logged Out.");
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

        public async Task<Response<string>> GeneretPasswordResetAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var url = $"https://tupaweb.azurewebsites.net/Login/Reset-Password?email=" + HttpUtility.UrlEncode(email) + "&token=" + HttpUtility.UrlEncode(token);

                string subject = "Alterar Senha";

                await _emailService.SendEmailAsync(
                    user.Email,
                    _emailService.templateEmail(
                        subject,
                        user.UserName,
                        "Clique no botão abaixo para resetar e trocar a sua senha.",
                        "Se você recebeu está mensagem por engano, simplesmente ignore este e-mail e não clique no botão.",
                        url,
                        "Mudar senha!"),
                    subject);

                return new Response<string>("", message: $"Successfully generated token.");
            }

            return new Response<string>(message: $"This email was not registered.");
        }

        public async Task<Response<string>> ChangePasswordAsync(string email, string token, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user != null)
            {
                var passwordValidator = new PasswordValidator<ApplicationUser>();
                var resultValidator = await passwordValidator.ValidateAsync(_userManager, null, password);

                if (resultValidator.Succeeded)
                {
                    var result = await _userManager.ResetPasswordAsync(user, token, password);

                    if (result.Succeeded)
                    {
                        return new Response<string>("", message: $"Success when changing password");
                    }
                } else
                {
                    return new Response<string>(message: $"Password not valid.");
                }
            }

            return new Response<string>(message: $"Failed to change password.");
        }

        public async Task<Response<string>> ChangePasswordWithIdAsync(string UserId, string oldPassword, string newPassword)
        {
            var usuario = _context.Usuario.Where(x => x.Id == UserId).FirstOrDefault();

            var user = await _userManager.FindByIdAsync(usuario.ApplicationUserID);

            if (user != null)
            {
                // verifica se a senha passada é correta
                var checkPassword = await _userManager.CheckPasswordAsync(user, oldPassword);

                if (checkPassword)
                {
                    var passwordValidator = new PasswordValidator<ApplicationUser>();
                    var resultValidator = await passwordValidator.ValidateAsync(_userManager, null, newPassword);

                    if (resultValidator.Succeeded)
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                        var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

                        if (result.Succeeded)
                        {
                            return new Response<string>("", message: $"Success when changing password");
                        }

                        return new Response<string>(message: $"Failed to change password.");
                    } else
                    {
                        return new Response<string>(message: $"Password not valid.");
                    }
                }
            }

            return new Response<string>(message: $"This user was not registered.");
        }
    
        public async Task<Response<IDictionary<string, string>>> GetBasicProfile(string UserId)
        {
            var usuario = _context.Usuario.Where(x => x.Id == UserId).Include(i => i.TipoUsuario).FirstOrDefault();

            var user = await _userManager.FindByIdAsync(usuario.ApplicationUserID);

            if (user != null)
            {
                var response = new Dictionary<string, string>();

                response.Add("UserName", user.UserName);
                response.Add("Nome", usuario.Nome);
                response.Add("Email", user.Email);
                response.Add("EmailConfirmed", user.EmailConfirmed.ToString());
                response.Add("TipoUsuario", (usuario.TipoUsuario != null) ? usuario.TipoUsuario.Descricao.ToString() : EnumTipoUsuario.Comum.ToString());

                return new Response<IDictionary<string, string>>(response, message: $"Success.");
            }

            return new Response<IDictionary<string, string>>(message: $"This user was not registered.");
        }

        public async Task<Response<string>> UpdateBasicProfile(string UserId, string UserName, string TipoUsuario)
        {
            try
            {
                var usuario = _context.Usuario.Where(x => x.Id == UserId).FirstOrDefault();

                var user = await _userManager.FindByIdAsync(usuario.ApplicationUserID);

                if (user != null)
                {
                    EnumTipoUsuario enumTipoUsuario;
                    Enum.TryParse(TipoUsuario.ToString(), out enumTipoUsuario);

                    var tipoUsuario = _context.TipoUsuario.Where(x => x.Descricao == enumTipoUsuario).FirstOrDefault();

                    if (tipoUsuario != null)
                    {
                        usuario.Nome = UserName;
                        usuario.TipoUsuario = tipoUsuario;
                        _context.SaveChanges();

                        return new Response<string>("", message: $"Success.");
                    }
                    else
                    {
                        return new Response<string>(message: $"Invalid user type.");
                    }
                }

                return new Response<string>(message: $"This user was not registered.");
            } catch (Exception)
            {
                return new Response<string>(message: $"Error: ");
            }
        }

        public async Task<Response<string>> DeleteAsync(string UserId)
        {
            var usuario = _context.Usuario.Where(x => x.Id == UserId).FirstOrDefault();

            var identityUserTask = _userManager.Users;
            var user = identityUserTask.Include(x => x.RefreshTokens).Include(x => x.Usuario)
                .FirstOrDefault(x => x.Id == usuario.ApplicationUserID);

            if (user != null)
            {
                // Apagar todos os seus dados
                var usuarioContext = _context.Usuario.Where(x => x.Id == usuario.Id).FirstOrDefault();

                // marcador
                var marcadorContext = _context.Marcadores.Where(x => x.Usuario == usuarioContext);

                _context.Marcadores.RemoveRange(marcadorContext);

                // historico
                var historicoContext = _context.HistoricoUsuario.Where(x => x.Usuario == usuarioContext);

                _context.HistoricoUsuario.RemoveRange(historicoContext);

                _context.HistoricoUsuario.RemoveRange(historicoContext);

                // usuario
                _context.Usuario.Remove(usuarioContext);

                // Refresh Tokens
                user.RefreshTokens.RemoveAll(x => x.UserId == usuario.ApplicationUserID);

                // Apagar Usuário
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    _context.SaveChanges();

                    return new Response<string>("", message: $"User deleted successfully.");
                }
                else
                {
                    return new Response<string>(message: $"Failed to delete user.");
                }
            }

            return new Response<string>(message: $"This user was not registered.");
        }

        public async Task<Response<string>> GetProfileImage(string UserId)
        {
            var usuario = _context.Usuario.Where(x => x.Id == UserId).Include(i => i.FotoPerfil).FirstOrDefault();

            var user = await _userManager.FindByIdAsync(usuario.ApplicationUserID);

            if (user != null)
            {
                var FotoPerfil = Convert.ToBase64String(usuario.FotoPerfil != null ? usuario.FotoPerfil.DataImagem : new byte[0]);

                return new Response<string>(FotoPerfil, message: $"Success.");
            }

            return new Response<string>(message: $"This user was not registered.");
        }

        // 512Kb
        private readonly int MAX_SIZE_IMAGE = 524288;

        public async Task<Response<string>> UpdateProfileImage(string UserId, string base64Image)
        {
            try
            {
                var usuario = _context.Usuario.Where(x => x.Id == UserId).FirstOrDefault();

                var user = await _userManager.FindByIdAsync(usuario.ApplicationUserID);

                if (user != null)
                {
                    var byteArray = Convert.FromBase64String(base64Image);

                    if (byteArray.Length > MAX_SIZE_IMAGE)
                    {
                        return new Response<string>(message: $"Exceeded maximum image size.");
                    }

                    usuario.FotoPerfil = new Imagem
                    {
                        DataImagem = byteArray
                    };
                    _context.SaveChanges();

                    return new Response<string>("", message: $"Success.");
                }

                return new Response<string>(message: $"This user was not registered.");
            }
            catch (Exception)
            {
                return new Response<string>(message: $"Error: ");
            }
        }
    }
}
