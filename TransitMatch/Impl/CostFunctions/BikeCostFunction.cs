using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureMapsToolkit.Common;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl.CostFunctions
{
    public class BikeCostFunction : BaseCostFunction
    {
        protected override double EstimateMonetaryCost(RouteDirectionsSummary routeDirectionsSummary)
        {
            return 0;
        }

        protected override double EstimateTimeCost(RouteDirectionsSummary routeDirectionsSummary)
        {
            return routeDirectionsSummary.TravelTimeInSeconds * 2;
        }

        protected override NavigationMode NavigationMode => NavigationMode.Bike;

        public BikeCostFunction(IMapsService mapsService) : base(mapsService)
        {
        }
    }
}