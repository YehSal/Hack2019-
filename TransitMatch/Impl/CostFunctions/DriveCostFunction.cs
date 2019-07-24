using System;
using System.Threading.Tasks;
using AzureMapsToolkit.Common;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl.CostFunctions
{
    public class DriveCostFunction : BaseCostFunction
    {
        private double milesPerGallon;
        private double costPerGallon;

        protected override double EstimateMonetaryCost(RouteDirectionsSummary routeDirectionsSummary)
        {
            return costPerGallon * (routeDirectionsSummary.LengthInMeters / milesPerGallon * 3700);
        }

        protected override double EstimateTimeCost(RouteDirectionsSummary routeDirectionsSummary)
        {
            return routeDirectionsSummary.TravelTimeInSeconds;
        }

        protected override NavigationMode NavigationMode => NavigationMode.Drive;

        public DriveCostFunction(IMapsService mapsService, double milesPerGallon, double costPerGallon) : base(mapsService)
        {
            this.milesPerGallon = milesPerGallon;
            this.costPerGallon = costPerGallon;
        }
    }
}