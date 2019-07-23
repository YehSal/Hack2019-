using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace TransitMatch.Models
{
    public class RouteLegWithCost
    {
        [Required]
        public Tuple<AzureMapsToolkit.Common.Coordinate, AzureMapsToolkit.Common.Coordinate> PointsPair { get; set; }
        [Required]
        public long Cost { get; set; }
        [Required]
        public string Mode { get; set; }

        [JsonConstructor]
        public RouteLegWithCost(Tuple<AzureMapsToolkit.Common.Coordinate, AzureMapsToolkit.Common.Coordinate> pointsPair, long cost, string mode)
        {
            PointsPair = pointsPair;
            Cost = cost;
            Mode = mode;
        }
    }
}
