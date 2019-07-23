using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        public NavigationRoutingServiceImpl(
            IRouteSegmentationService routeSegmentationService,
            INavigationCostGeneratorService navigationCostGeneratorService,
            IPathFindingService pathFindingService)
        {
            _routeSegmentationService = routeSegmentationService;
            _navigationCostGeneratorService = navigationCostGeneratorService;
            _pathFindingService = pathFindingService;
        }

        public async Task<ActionResult<List<RoutingSegment>>> GetOptimalRoute(NavigationRequestParam navigationParams)
        {
            Console.WriteLine("Finding your optimal path!");
            var routeSegments =
               await _routeSegmentationService.GetSegments(navigationParams.StartPoint, navigationParams.EndPoint);
            var costMatrix = await this.GenerateCostMatrix(routeSegments, navigationParams.Optimizer);
            var optimalRoute = _pathFindingService.GetOptimalPath(routeSegments, costMatrix);
            Console.WriteLine(optimalRoute);
            Console.WriteLine(navigationParams.ToString());
            var testResponse = new List<RoutingSegment>
            {
                new RoutingSegment(
                    new NavigationPoint(0, 0),
                    new NavigationPoint(10, 10),
                    NavigationMode.Walk)
            };
            return testResponse;
        }

        private async Task<double[,]> GenerateCostMatrix(List<Tuple<NavigationPoint, NavigationPoint>> routeSegments, OptimizationParam optimizer)
        {
            var navigationModes = EnumUtils.GetEnumValues<NavigationMode>() as NavigationMode[] ?? EnumUtils.GetEnumValues<NavigationMode>().ToArray();
            var costMatrix = new double[navigationModes.Length, routeSegments.Count];
            var costFunctionTasks = new Task<double>[navigationModes.Length * routeSegments.Count];
            for (var i = 0; i < navigationModes.Length; i++)
            {
               for (var j = 0; j < routeSegments.Count; j++)
               {
                   var j1 = j;
                   var i1 = i;
                   costFunctionTasks[i * j] = Task.Run(() => _navigationCostGeneratorService.GetCostForSegment(
                       new RoutingSegment(routeSegments[j1].Item1,
                           routeSegments[j1].Item2,
                           navigationModes[i1]),
                       optimizer));
               }
            }

            var result = await Task.WhenAll(costFunctionTasks);
            for (var i = 0; i < result.Length; i++)
            {
                costMatrix[i % navigationModes.Length, i / navigationModes.Length] = result[i];
            }
            return costMatrix;
        }
    }
}
