using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureMapsToolkit.Common;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl.CostFunctions
{
    public abstract class BaseCostFunction : ICostFunction
    {
        protected abstract double EstimateMonetaryCost(RouteDirectionsSummary routeDirectionsSummary);

        protected abstract double EstimateTimeCost(RouteDirectionsSummary routeDirectionsSummary);

        protected abstract NavigationMode NavigationMode { get; }

        protected readonly IMapsService _mapsService;

        protected BaseCostFunction(IMapsService mapsService)
        {
            _mapsService = mapsService;
        }

        public async Task<RoutingSegmentResult> GetCost(NavigationPoint start, NavigationPoint end, OptimizationParam optimizer)
        {
            // Call API from start to end to figure how much time it'll take 
            var routingResult = await _mapsService.GetRouteDirections(start, end, NavigationMode);
            var routingCost = GetCostFromRouteSummary(routingResult.Summary, optimizer);

            return new RoutingSegmentResult(NavigationMode.ToString(), GetNavigationPointsFromLegs(routingResult.Legs), routingCost);
        }

        public double GetCostFromRouteSummary(RouteDirectionsSummary routeDirectionsSummary, OptimizationParam optimizer)
        {
            return EstimateMonetaryCost(routeDirectionsSummary) *
                   (OptimizationParam.MaxValue - optimizer.OptimizerValue) +
                   EstimateTimeCost(routeDirectionsSummary) * optimizer.OptimizerValue;
        }

        protected List<NavigationPoint> GetNavigationPointsFromLegs(RouteResultLeg[] legs)
        {
            var routingSegmentPoints = new List<NavigationPoint>();
            foreach (var leg in legs)
            {
                routingSegmentPoints.AddRange(leg.Points.Select(point => (new NavigationPoint(point.Latitude, point.Longitude))));
            }

            return routingSegmentPoints;
        }
    }
}