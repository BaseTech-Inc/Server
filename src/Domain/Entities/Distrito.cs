namespace Domain.Entities
{
    public class Distrito
    {
        public string Id { get; set; }

        public Cidade Cidade { get; set; }

        public string Nome { get; set; }
    }
}
