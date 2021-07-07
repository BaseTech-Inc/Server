using System;
using System.Globalization;
namespace Application.GeoJson.Geometry
{
    public class Position : IPosition
    {
        public Position(double latitude, double longitude, double? altitude = null)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }
        public Position(string latitude, string longitude, string altitude = null)
        {
            if (string.IsNullOrEmpty(latitude))
            {
                throw new ArgumentOutOfRangeException(nameof(latitude), "May not be empty.");
            }

            if (string.IsNullOrEmpty(longitude))
            {
                throw new ArgumentOutOfRangeException(nameof(longitude), "May not be empty.");
            }

            if (!double.TryParse(latitude, NumberStyles.Float, CultureInfo.InvariantCulture, out double lat))
            {
                throw new ArgumentOutOfRangeException(nameof(altitude), "Latitude representation must be a numeric.");
            }

            if (!double.TryParse(longitude, NumberStyles.Float, CultureInfo.InvariantCulture, out double lon))
            {
                throw new ArgumentOutOfRangeException(nameof(altitude), "Longitude representation must be a numeric.");
            }

            Latitude = lat;
            Longitude = lon;

            if (altitude != null)
            {
                if (!double.TryParse(altitude, NumberStyles.Float, CultureInfo.InvariantCulture, out double alt))
                {
                    throw new ArgumentOutOfRangeException(nameof(altitude), "Altitude must be a proper altitude (m(eter) as double) value, e.g. '6500'.");
                }

                Altitude = alt;
            }
        }

        public double? Altitude { get; }
        public double Latitude { get; }
        public double Longitude { get; }

        public override string ToString()
        {
            return Altitude == null
                ? string.Format(CultureInfo.InvariantCulture, "Latitude: {0}, Longitude: {1}", Latitude, Longitude)
                : string.Format(CultureInfo.InvariantCulture, "Latitude: {0}, Longitude: {1}, Altitude: {2}", Latitude, Longitude, Altitude);
        }
    }
}
