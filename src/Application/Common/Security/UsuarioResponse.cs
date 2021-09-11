namespace Application.Common.Security
{
    public class UsuarioResponse
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string TipoUsuario { get; set; }
    }
}
