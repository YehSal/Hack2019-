using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuickGraph;
using QuickGraph.Serialization.DirectedGraphML;
using TransitMatch.Common;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl
{
    public class NavigationRoutingServiceImpl : INavigationRoutingService
    {
        private readonly IRouteSegmentationService _routeSegmentationService;
        private readonly IPathFindingService _pathFindingService;
        private readonly INavigationCostGeneratorService _navigationCostGeneratorService;
        private readonly RoutingSegmentCache _internalCache = new RoutingSegmentCache();

        public NavigationRoutingServiceImpl(
            IRouteSegmentationService routeSegmentationService,
            INavigationCostGeneratorService navigationCostGeneratorService,
            IPathFindingService pathFindingService)
        {
            _routeSegmentationService = routeSegmentationService;
            _navigationCostGeneratorService = navigationCostGeneratorService;
            _pathFindingService = pathFindingService;
        }

        public async Task<ActionResult<List<RoutingSegmentResult>>> GetOptimalRoute(NavigationRequestParam navigationParams)
        {
            Console.WriteLine("Finding your optimal path!");
            var routeGraph =
               await _routeSegmentationService.GetGraph(navigationParams.StartPoint, navigationParams.EndPoint);
            var weightedGraph = await GenerateCosts(routeGraph, navigationParams.Optimizer);
            var optimalRoute = _pathFindingService.GetOptimalPath(navigationParams.StartPoint, navigationParams.EndPoint, weightedGraph);
            return optimalRoute.Select(segment => _internalCache.GetByKey(segment)).ToList();
            // TODO: return optimalRoute
            // TODO: Remove below test debug return
            Console.WriteLine(optimalRoute);
            Console.WriteLine(navigationParams.ToString());
            var testResponse = new List<RoutingSegmentResult>
            {
                new RoutingSegmentResult(
                    Models.NavigationMode.Walk.ToString(),
                    new List<NavigationPoint>(){ new NavigationPoint(0, 0), new NavigationPoint(10,0) },
                    0
                    )
            };
            return testResponse;
        }

        private async Task<AdjacencyGraph<NavigationPoint, WeightedEdge<NavigationPoint>>> GenerateCosts(
            AdjacencyGraph<NavigationPoint, TransportEdge<NavigationPoint>> segmentedGraph, OptimizationParam optimizer)
        {
            var weightedGraph = new AdjacencyGraph<NavigationPoint, WeightedEdge<NavigationPoint>>();
            var weightingTasks = new List<Task<WeightedEdge<NavigationPoint>>>(
                segmentedGraph.Edges.Select((async edge =>
                {
                    var segment = new RoutingSegment(edge.Source,
                        edge.Target,
                        edge.NavigationMode);
                    var costedRoute = await _navigationCostGeneratorService.GetCostForSegment(segment, optimizer);
                    _internalCache.Add(segment, costedRoute);
                    return new WeightedEdge<NavigationPoint>(edge.Source, edge.Target, edge.NavigationMode, costedRoute.cost);
                })));
            weightedGraph.AddVerticesAndEdgeRange(await Task.WhenAll(weightingTasks));
            return weightedGraph;
        }
    }
}
