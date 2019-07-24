using System;
using System.Collections.Generic;
using QuickGraph;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface IPathFindingService
    {
        List<RoutingSegment> GetOptimalPath(NavigationPoint start, NavigationPoint end, AdjacencyGraph<NavigationPoint, WeightedEdge<NavigationPoint>> weightedGraph);
    }
}