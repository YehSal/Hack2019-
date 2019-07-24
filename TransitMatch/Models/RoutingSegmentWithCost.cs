using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using System;
using AzureMapsToolkit.Common;
using TransitMatch.Common;

namespace TransitMatch.Models
{
    public class RoutingSegmentWithCost : RoutingSegment
    {
        public RoutingSegmentWithCost(Coordinate startPoint, Coordinate endPoint, String mode, double cost) 
            : base(
                new NavigationPoint(startPoint.Latitude, startPoint.Longitude),
                new NavigationPoint(endPoint.Latitude, endPoint.Longitude),
                EnumUtils.parseFromString(mode))
        {
            Cost = cost;
        }

        [Required]
        public double Cost { get; }


    }
}
