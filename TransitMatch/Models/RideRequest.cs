using System.Collections.Generic;
using AzureMapsToolkit.Common;

namespace TransitMatch.Models
{
    public class RideRequests
    {
        public string Copyright;
        public string FormatVersion;
        public List<RouteOptimizedWaypoint> OptimizedWaypoints;
        public string Privacy;
        public RouteResponseReport Report;
        public List<RouteDirectionsResult> Routes;
    }
}
