using System;
using System.Collections.Generic;

namespace TransitMatch.Models
{
    public class RoutingSegmentResult
    {
        public RoutingSegmentResult(string mode, List<NavigationPoint> routePoints, double cost)
        {
            this.mode = mode;
            this.routePoints = routePoints;
            this.cost = cost;
        }

        public string mode { get; }
        public List<NavigationPoint> routePoints { get; }
        public double cost { get; }
    }
}
