using System;
using System.Threading.Tasks;
using AzureMapsToolkit.Common;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl.CostFunctions
{
    public class TransitCostFunction : BaseCostFunction
    {
        public TransitCostFunction(IMapsService mapsService) : base(mapsService)
        {
        }

        protected override double EstimateMonetaryCost(RouteDirectionsSummary routeDirectionsSummary)
        {
            return 2;
        }

        protected override double EstimateTimeCost(RouteDirectionsSummary routeDirectionsSummary)
        {
            return routeDirectionsSummary.TravelTimeInSeconds;
        }

        protected override NavigationMode NavigationMode => NavigationMode.Transit;
    }
} 