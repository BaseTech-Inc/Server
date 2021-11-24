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
using System.Net.Mail;

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
            try
            {
                var m = new MailAddress(email);
            }
            catch
            {
                return new Response<LoginResponse>(message: $"Email inválido.");
            }

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

                        return new Response<LoginResponse>(response, message: $"Autenticado { user.UserName }");
                    }
                }
            }

            return new Response<LoginResponse>(message: $"Ocorreu um erro ao autenticar o usuário.");
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
                    try
                    {
                        var m = new MailAddress(email);
                    } catch
                    {
                        return new Response<string>(message: $"Email inválido.");
                    }

                    var listUsuario = new List<Usuario>();

                    string imgBase64 = "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";

                    var usuario = new Usuario
                    {
                        Email = email,
                        TipoUsuario = new TipoUsuario
                        {
                            Descricao = EnumTipoUsuario.Comum
                        },
                        Nome = username,
                        FotoPerfil = new Imagem
                        {
                            DataImagem = Encoding.ASCII.GetBytes(imgBase64),
                            TituloImagem = "PerfilPhoto"
                        }
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

                        return new Response<string>(usuario.Id, message: $"Usuário registrado.");
                    }
                } else
                {
                    return new Response<string>(message: $"Password not valid.");
                }                
            } 
            else
            {
                return new Response<string>(message: $"Você já tem um usuário registrado com esta credencial.");
            }

            return new Response<string>(message: $"Erro durante o registro.");
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
                return new Response<LoginResponse>(message: $"Fracassado.");
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

            return new Response<LoginResponse>(response, message: $"Sucesso.");
        }

        public async Task<Response<string>> RevokeToken(HttpContext httpContext, string token)
        {
            var result = await _tokenService.RevokeRefreshToken(httpContext, token);

            // If user found, then revoke
            if (result)
            {
                return new Response<string>("", message: $"Sucesso.");
            }

            // Otherwise, return error
            return new Response<string>(message: $"Fracassado.");
        }

        public async Task<Response<string>> LogoutAsync(HttpContext httpContext)
        {
            // Revoke Refresh Token 
            await _tokenService.RevokeRefreshToken(httpContext);

            return new Response<string>("", message: $"Desconectado.");
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
                    return new Response<string>(usuario.Id, message: $"Email confirmado com sucesso.");
                }
            }

            return new Response<string>(message: $"Falha ao verificar o e-mail.");
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

                return new Response<string>("", message: $"Token gerado com sucesso.");
            }

            return new Response<string>(message: $"Este email não foi cadastrado.");
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
                        return new Response<string>("", message: $"Sucesso ao alterar a senha.");
                    }
                } else
                {
                    return new Response<string>(message: $"Senha inválida.");
                }
            }

            return new Response<string>(message: $"Falha ao alterar a senha.");
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
                            return new Response<string>("", message: $"Sucesso ao alterar a senha");
                        }

                        return new Response<string>(message: $"Falha ao alterar a senha.");
                    } else
                    {
                        return new Response<string>(message: $"Senha inválida.");
                    }
                }
            }

            return new Response<string>(message: $"Este usuário não foi registrado.");
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

                return new Response<IDictionary<string, string>>(response, message: $"Sucesso.");
            }

            return new Response<IDictionary<string, string>>(message: $"Este usuário não foi registrado.");
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

                        return new Response<string>("", message: $"Sucesso.");
                    }
                    else
                    {
                        return new Response<string>(message: $"Tipo de usuário inválido.");
                    }
                }

                return new Response<string>(message: $"Este usuário não foi registrado.");
            } catch (Exception)
            {
                return new Response<string>(message: $"Erro.");
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

                    return new Response<string>("", message: $"Usuário excluído com sucesso.");
                }
                else
                {
                    return new Response<string>(message: $"Falha ao excluir usuário.");
                }
            }

            return new Response<string>(message: $"Este usuário não foi registrado.");
        }

        public async Task<Response<string>> GetProfileImage(string UserId)
        {
            var usuario = _context.Usuario.Where(x => x.Id == UserId).Include(i => i.FotoPerfil).FirstOrDefault();

            var user = await _userManager.FindByIdAsync(usuario.ApplicationUserID);

            if (user != null)
            {
                var FotoPerfil = Convert.ToBase64String(usuario.FotoPerfil != null ? usuario.FotoPerfil.DataImagem : new byte[0]);

                return new Response<string>(FotoPerfil, message: $"Sucesso.");
            }

            return new Response<string>(message: $"Este usuário não foi registrado.");
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
                        return new Response<string>(message: $"Excedeu o tamanho máximo da imagem.");
                    }

                    usuario.FotoPerfil = new Imagem
                    {
                        DataImagem = byteArray
                    };
                    _context.SaveChanges();

                    return new Response<string>("", message: $"Sucesso.");
                }

                return new Response<string>(message: $"Este usuário não foi registrado.");
            }
            catch (Exception)
            {
                return new Response<string>(message: $"Erro");
            }
        }
    }
}
