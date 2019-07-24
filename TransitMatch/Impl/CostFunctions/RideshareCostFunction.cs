using System;
using System.Threading.Tasks;
using AzureMapsToolkit.Common;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl.CostFunctions
{
    public class RideshareCostFunction : BaseCostFunction
    {
        public RideshareCostFunction(IMapsService mapsService) : base(mapsService)
        {
        }

        protected override double EstimateMonetaryCost(RouteDirectionsSummary routeDirectionsSummary)
        {
            // TODO: Jin add code here
            return 0;
        }

        protected override double EstimateTimeCost(RouteDirectionsSummary routeDirectionsSummary)
        {
            return routeDirectionsSummary.TravelTimeInSeconds;
        }

        protected override NavigationMode NavigationMode => NavigationMode.Rideshare;
    }
}