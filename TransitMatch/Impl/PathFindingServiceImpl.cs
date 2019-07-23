using System;
using System.Collections.Generic;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl
{
    public class PathFindingServiceImpl: IPathFindingService
    {
        public List<RoutingSegment> GetOptimalPath(List<Tuple<NavigationPoint, NavigationPoint>> routeSegments, double[,] costMatrix)
        {
            // TODO: Implement
            return new List<RoutingSegment>();
        }
    }
}