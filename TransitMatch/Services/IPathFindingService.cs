using System;
using System.Collections.Generic;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface IPathFindingService
    {
        List<RoutingSegment> GetOptimalPath(List<Tuple<NavigationPoint, NavigationPoint>> routeSegments, double[,] costMatrix);
    }
}