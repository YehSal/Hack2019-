using System.ComponentModel.DataAnnotations;

namespace TransitMatch.Models
{
    public class NavigationPoint
    {
        [Required]
        [Display(Name = "Latitude")]
        public long LatitudeInt64 { get; }
        [Required]
        [Display(Name = "Longitude")]
        public long LongitudeInt64 { get; }

        public NavigationPoint(long latitudeInt64, long longitudeInt64)
        {
            LatitudeInt64 = latitudeInt64;
            LongitudeInt64 = longitudeInt64;
        }
    }
}
