using AzureMapsToolkit;
using Microsoft.Extensions.DependencyInjection;
using TransitMatch.Impl;
using TransitMatch.Impl.Azure;

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
            services.AddSingleton(new KeyVaultClientFactory());
            services.AddSingleton<IMapsService, AzureMapsService>();
        }
    }
}
