using Domain.Common;

namespace Domain.Entities
{
    public class Ponto : CountEntity
    {
        public string Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
