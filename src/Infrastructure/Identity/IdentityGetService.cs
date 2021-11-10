using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using Domain.Enumerations;
using Domain.Entities;
using System;

namespace Infrastructure.Identity
{
    public class IdentityGetService : IIdentityGetService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IApplicationDbContext _context;

        public IdentityGetService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public async Task<Response<IList<UsuarioResponse>>> GetAllIdentity()
        {
            try
            {
                var usuarios = _context.Usuario.ToList();
                var usuarioDtoList = new List<UsuarioResponse>();

                foreach (var usuario in usuarios)
                {
                    var user = await _userManager.FindByIdAsync(usuario.ApplicationUserID);

                    if (user != null)
                    {
                        usuarioDtoList.Add(new UsuarioResponse
                        {
                            UserId = usuario.Id,
                            UserName = user.UserName,
                            Email = user.Email,
                            EmailConfirmed = user.EmailConfirmed,
                            TipoUsuario = (usuario.TipoUsuario != null) ? usuario.TipoUsuario.Descricao.ToString() : EnumTipoUsuario.Comum.ToString()
                        });
                    }                    
                }

                return new Response<IList<UsuarioResponse>>(usuarioDtoList, message: $"");
            } catch (Exception ex)
            {
                return new Response<IList<UsuarioResponse>>(message: $"erro para obter: " + ex);
            }
        }
            
    }
}
