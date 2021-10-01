namespace Domain.Entities
{
    public class Usuario
    {
        public string Id { get; set; }

        public TipoUsuario TipoUsuario { get; set; }

        public string Nome { get; set; }

        public string ContaBancaria { get; set; }

        public string Email { get; set; }

        public string ApplicationUserID { get; set; }
    }
}
