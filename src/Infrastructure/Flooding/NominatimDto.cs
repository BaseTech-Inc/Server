namespace Infrastructure.Flooding
{
    public class NominatimDto
    {
        public int place_id { get; set; }

        public string lat { get; set; }

        public string lon { get; set; }

        public Address address { get; set; }
    }

    public class Address
    {
        public string suburb { get; set; }

        public string city { get; set; }

        public string state { get; set; }
    }
}
