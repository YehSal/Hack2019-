using System;
using System.Net.Http;
using System.Threading.Tasks;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl.Azure
{
    public class AzureMapsService : BaseAzureService, IMapsService
    {
        public AzureMapsService(KeyVaultClientFactory kcClientFactory) : base(kcClientFactory)
        {
        }

        public Task<HttpResponseMessage> GetRouteDirections(NavigationPoint start, NavigationPoint end)
        {
            Console.WriteLine("Getting resource from Azure");
            return GetAzureResource("atlas", "route/directions/json", $"query={start.Latitude},{start.Longitude}:{end.Latitude},{end.Longitude}");
        }

        public async Task<HttpResponseMessage> GetNearbyTransit(NavigationPoint point)
        {
            var metroIdResponse = await GetAzureResource("atlas", "mobility/metroArea/id/json", $"query=${point.Latitude},${point.Longitude}");
            var metroId = await metroIdResponse.Content.ReadAsAsync<>();
        }

        protected override string SecretResourcePath()
        {
            return "Maps";
        }
    }
}
