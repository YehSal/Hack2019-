using System;
using System.Threading.Tasks;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl.CostFunctions
{
    public class TransitCostFunction : ICostFunction
    {
        public async Task<double> GetCost(NavigationPoint start, NavigationPoint end, OptimizationParam optimizer)
        {
            // TODO: Implement
            return 0;
        }
    }
}