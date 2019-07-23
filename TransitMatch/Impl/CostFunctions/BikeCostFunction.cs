using System;
using System.Threading.Tasks;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl.CostFunctions
{
    public class BikeCostFunction : ICostFunction
    {
        public async Task<double> GetCost(NavigationPoint start, NavigationPoint end, OptimizationParam optimizer)
        {
            int monetaryCost = 0; // No cost if you own a bike

            // Call API from start to end to figure how much time it'll take 
            

            return 0;
        }
    }
}