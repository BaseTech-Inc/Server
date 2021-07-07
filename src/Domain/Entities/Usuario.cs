namespace Domain.Entities
{
    public class Usuario
    {
        public string Id { get; set; }

        public TipoUsuario TipoUsuario { get; set; }

        public string ContaBancaria { get; set; }
    }
}
