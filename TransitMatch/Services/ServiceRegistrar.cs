using Microsoft.Extensions.DependencyInjection;
using TransitMatch.Impl;

namespace TransitMatch.Services
{
    public class ServiceRegistrar
    {
        public static void RegisterTransitMatchServices(IServiceCollection services)
        {

            services.AddScoped<INavigationRoutingService, NavigationRoutingServiceImpl>();
            services.AddSingleton<IRouteSegmentationService, RouteSegmentationServiceImpl>();
            services.AddSingleton<ICostFunctionFactory, CostFunctionFactoryImpl>();
            services.AddSingleton<INavigationCostGeneratorService, NavigationCostGeneratorServiceImpl>();
            services.AddSingleton<IPathFindingService, PathFindingServiceImpl>();
        }
    }
}
