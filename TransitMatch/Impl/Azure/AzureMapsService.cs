using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AzureMapsToolkit.Common;
using TransitMatch.Models;
using TransitMatch.Services;


namespace TransitMatch.Impl.Azure
{
    public class AzureMapsService : BaseAzureService, IMapsService
    {
        public AzureMapsService(KeyVaultClientFactory kcClientFactory) : base(kcClientFactory)
        {
        }

        public async Task<RouteDirectionsResult> GetRouteDirections(NavigationPoint start, NavigationPoint end,
            NavigationMode navMode, int numRoutes = 3)
        {
            string travelMode;
            switch (navMode)
            {
                case NavigationMode.Drive:
                case NavigationMode.Rideshare:
                    travelMode = "car";
                    break;
                case NavigationMode.Transit:
                    travelMode = "bus";
                    break;
                default:
                    travelMode = "car";
                    break;
            }

            var routeResult = await GetAzureResource("atlas",
                "route/directions/json",
                $"query={start.Latitude},{start.Longitude}:{end.Latitude},{end.Longitude}&traffic=true&routeType=fastest&maxAlternatives={numRoutes-1}&travelMode={travelMode}");
            var resultContainer = await routeResult.Content.ReadAsAsync<RouteDirectionsResponse>();
            Console.WriteLine(resultContainer.Routes.ToArray());
            return resultContainer.Routes[0];
        }

        public async Task<List<NavigationPoint>> GetNearbyTransit(NavigationPoint point)
        {
            var metroIdResponse = await GetAzureResource("atlas", "mobility/metroArea/id/json", $"query={point.Latitude},{point.Longitude}");
            Console.WriteLine(await metroIdResponse.Content.ReadAsStringAsync());
            var metroId = await metroIdResponse.Content.ReadAsAsync<AzureResponseContainer<MetroAreaResult>>();
            Console.WriteLine(metroId.results[0].metroName);
            var busStopResult = await GetAzureResource("atlas",
                "mobility/transit/nearby/json",
                $"metroId={metroId.results[0].metroId}&query={point.Latitude},{point.Longitude}&objectType=stop&limit=2&radius=500");
            var busStopsResult = await busStopResult.Content.ReadAsAsync<AzureResponseContainer<NearbyTransitResult>>();
            var busStops = busStopsResult.results;
            Console.WriteLine(busStops.ToString());
            return busStops.Select((busStop => busStop.position)).ToList();
        }

        protected override string SecretResourcePath()
        {
            return "Maps";
        }
    }
}
