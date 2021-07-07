using Domain.Enumerations;

namespace Domain.Entities
{
    public class TipoUsuario
    {
        public string Id { get; set; }

        public EnumTipoUsuario Descricao { get; set; }
    }
}
