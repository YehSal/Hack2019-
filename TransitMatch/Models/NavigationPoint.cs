using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TransitMatch.Models
{
    public class NavigationPoint
    {
        [Required]
        public double Latitude { get; }
        [Required]
        public double Longitude { get; }

        [JsonConstructor]
        public NavigationPoint(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
