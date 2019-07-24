using System.Threading.Tasks;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl
{
    public class NavigationCostGeneratorServiceImpl : INavigationCostGeneratorService
    {
        private readonly ICostFunctionFactory _costFunctionFactory;

        public NavigationCostGeneratorServiceImpl(ICostFunctionFactory costFunctionFactory)
        {
            _costFunctionFactory = costFunctionFactory;
        }

        public Task<RoutingSegmentResult> GetCostForSegment(RoutingSegment segment, OptimizationParam optimizer)
        {
            var costFunction = _costFunctionFactory.GetCostFunctionByType(segment.SegmentNavigationMode);
            return costFunction.GetCost(segment.SegmentStart, segment.SegmentEnd, optimizer);
        }
    }

}
