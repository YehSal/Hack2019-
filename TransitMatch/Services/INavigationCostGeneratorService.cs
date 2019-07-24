using System;
using System.Threading.Tasks;
using TransitMatch.Models;

namespace TransitMatch.Services
{
    public interface INavigationCostGeneratorService
    {
        Task<RoutingSegmentResult> GetCostForSegment(RoutingSegment segment, OptimizationParam optimizer);
    }
}
