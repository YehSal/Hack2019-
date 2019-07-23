using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TransitMatch.Models
{
    public class NavigationPoint
    {
        [Required]
        public long Latitude { get; }
        [Required]
        public long Longitude { get; }

        [JsonConstructor]
        public NavigationPoint(long latitude, long longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
