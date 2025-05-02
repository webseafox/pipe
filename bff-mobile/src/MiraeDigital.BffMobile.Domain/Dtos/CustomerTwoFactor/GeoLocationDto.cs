namespace MiraeDigital.BffMobile.Domain.Dtos.CustomerTwoFactor
{
    public class GeoLocationDto
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public GeoLocationDto() { }
        public GeoLocationDto(decimal latitude, decimal longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
