using System;
using TransitMatch.Impl.CostFunctions;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl
{
    public class CostFunctionFactoryImpl :  ICostFunctionFactory
    {
        public ICostFunction GetCostFunctionByType(NavigationMode mode)
        {
            switch (mode)
            {
                case NavigationMode.Bike:
                    return new BikeCostFunction();
                case NavigationMode.Drive:
                    return new DriveCostFunction();
                case NavigationMode.Rideshare:
                    return new RideshareCostFunction();
                case NavigationMode.Transit:
                    return new TransitCostFunction();
                case NavigationMode.Walk:
                    return new WalkCostFunction();
                default:
                    throw new ArgumentException("Illegal navigation type");
            }
        }
    }
}
