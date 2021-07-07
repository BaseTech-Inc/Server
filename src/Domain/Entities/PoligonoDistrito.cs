namespace Domain.Entities
{
    public class PoligonoDistrito
    {
        public string Id { get; set; }

        public Poligono Poligono { get; set; }

        public Distrito Distrito { get; set; }
    }
}
