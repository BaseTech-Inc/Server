namespace Domain.Entities
{
    public class PoligonoPais
    {
        public string Id { get; set; }

        public Poligono Poligono { get; set; }

        public Pais Pais { get; set; }
    }
}
