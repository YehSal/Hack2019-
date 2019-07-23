using System;
using System.Threading.Tasks;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface INavigationCostGeneratorService
    {
        Task<double> GetCostForSegment(RoutingSegment segment, OptimizationParam optimizer);
    }
}
