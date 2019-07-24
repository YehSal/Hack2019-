using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms;
using QuickGraph.Algorithms.Search;
using QuickGraph.Algorithms.ShortestPath;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl
{
    public class PathFindingServiceImpl : IPathFindingService
    {
        public List<RoutingSegment> GetOptimalPath(NavigationPoint start, NavigationPoint end, AdjacencyGraph<NavigationPoint, WeightedEdge<NavigationPoint>> weightedGraph)
        {
            var shortestPathResult = weightedGraph.ShortestPathsBellmanFord(edge => edge.EdgeWeight, start);
            shortestPathResult(end, out var shortestPath);
            return shortestPath.Select((edge => new RoutingSegment(edge.Source,edge.Target, edge.NavigationMode)))
                .ToList();
        }
    }
}