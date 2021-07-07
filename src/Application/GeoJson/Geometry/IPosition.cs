namespace Application.GeoJson.Geometry
{
    public interface IPosition
    {
        double? Altitude { get; }
        double Latitude { get; }
        double Longitude { get; }
    }
}
