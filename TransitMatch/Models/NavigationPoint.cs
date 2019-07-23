using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace TransitMatch.Models
{
    public class NavigationPoint
    {
        [Required]
        public long Latitude { get; set;}
        [Required]
        public long Longitude { get; set;}

        [JsonConstructor]
        public NavigationPoint(long latitude, long longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
