namespace Domain.Entities
{
    public class PoligonoEstado
    {
        public string Id { get; set; }

        public Poligono Poligono { get; set; }

        public Estado Estado { get; set; }
    }
}
