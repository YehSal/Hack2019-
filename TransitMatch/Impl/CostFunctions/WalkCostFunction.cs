using System;
using System.Threading.Tasks;
using AzureMapsToolkit.Common;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl.CostFunctions
{
    public class WalkCostFunction : BaseCostFunction
    {
        public WalkCostFunction(IMapsService mapsService) : base(mapsService)
        {
        }

        protected override double EstimateMonetaryCost(RouteDirectionsSummary routeDirectionsSummary)
        {
            return 0;
        }

        protected override double EstimateTimeCost(RouteDirectionsSummary routeDirectionsSummary)
        {
            return routeDirectionsSummary.TravelTimeInSeconds * 4;
        }

        protected override NavigationMode NavigationMode => NavigationMode.Walk;
    }
}