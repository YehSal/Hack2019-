using System;
using TransitMatch.Impl.CostFunctions;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl
{
    public class CostFunctionFactoryImpl :  ICostFunctionFactory
    {
        private readonly IMapsService _mapsService;

        public CostFunctionFactoryImpl(IMapsService mapsService)
        {
            _mapsService = mapsService;
        }

        public ICostFunction GetCostFunctionByType(NavigationMode mode)
        {
            switch (mode)
            {
                case NavigationMode.Bike:
                    return new BikeCostFunction(_mapsService);
                case NavigationMode.Drive:
                    return new DriveCostFunction(_mapsService, 30, 3.75);
                case NavigationMode.Rideshare:
                    return new RideshareCostFunction(_mapsService);
                case NavigationMode.Transit:
                    return new TransitCostFunction(_mapsService);
                case NavigationMode.Walk:
                    return new WalkCostFunction(_mapsService);
                default:
                    throw new ArgumentException("Illegal navigation type");
            }
        }
    }
}
