namespace Domain.Entities
{
    public class PoligonoCidade
    {
        public string Id { get; set; }

        public Poligono Poligono { get; set; }

        public Cidade Cidade { get; set; }
    }
}
