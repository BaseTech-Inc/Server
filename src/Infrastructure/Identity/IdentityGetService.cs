using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Common.Security;
using Domain.Enumerations;

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
            var usuarios = _context.Usuario.ToList();
            var usuarioDtoList = new List<UsuarioResponse>();

            foreach (var usuario in usuarios)
            {
                var user = await _userManager.FindByIdAsync(usuario.ApplicationUserID);

                var usuarioDto = new UsuarioResponse
                {
                    UserId = usuario.Id,
                    UserName = user.UserName,
                    Email = user.Email,
                    EmailConfirmed = user.EmailConfirmed,
                    TipoUsuario = (usuario.TipoUsuario != null) ? usuario.TipoUsuario.Descricao.ToString() : EnumTipoUsuario.Comum.ToString()
                };

                usuarioDtoList.Add(usuarioDto);
            }

            return new Response<IList<UsuarioResponse>>(usuarioDtoList, message: $"");
        }
    }
}
