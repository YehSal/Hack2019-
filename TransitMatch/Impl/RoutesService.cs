using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AzureMapsToolkit.Common;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using TransitMatch.Models;
using TransitMatch.Services;

using System.Linq;

namespace TransitMatch.Impl
{
    public class RoutesService : IRoutesService
    {
        private readonly HttpClient client = new HttpClient();
        private IKeyVaultClient keyVaultClient;
        private readonly string AzureMapsSubscriptionKey = "yXcIJq3mB7bC-mdkyuKytBmnHGLHpVK2ewYL0sFJUbQ";

        // How to call Routes Service
        // var routeService = new RoutesService(new System.Net.Http.HttpClient(), null);
        // var result = routeService.GetRidesAsync(47.6445277,-122.1391559, 47.60972,-122.3443816, NavigationMode.Rideshare).Result;
        // foreach (var r in result)
        // {
        //     Console.WriteLine($"Length (m):\t{r.Summary.LengthInMeters}\tTime:\t{r.Summary.TravelTimeInSeconds}.");
        // }

        public RoutesService(HttpClient client, IKeyVaultClient keyVaultClient)
        {
            // this.client = client ?? throw new ArgumentNullException(nameof(client));
            // keyVaultClient = keyVaultClient ?? throw new ArgumentNullException(nameof(client));
        }


        // Microsoft Commons: 47.6445277,-122.1391559
        // Pike Place: 47.60972,-122.3443816
        // GetRoutes(47.6445277,-122.1391559, 47.60972,-122.3443816, "bus")
        public async Task<List<RouteDirectionsResult>> GetRidesAsync(double lat1, double lon1, double lat2, double lon2, NavigationMode mode)
        {
            var routes = new List<RouteDirectionsResult>();
            var response = await GetRoutesFromApiAsync(lat1, lon1, lat2, lon2, mode);
            var routesResponse = await response.Content.ReadAsAsync<RouteDirectionsResponse>();
            if (routesResponse != null && routesResponse.Routes.Length > 0)
            {
                routes = new List<RouteDirectionsResult>(routesResponse.Routes);
            }
            return routes;
        }

        // Microsoft Commons: 47.6445277,-122.1391559
        // Pike Place: 47.60972,-122.3443816
        // GetRoutes(47.6445277,-122.1391559, 47.60972,-122.3443816, "bus", 3)
        private async Task<HttpResponseMessage> GetRoutesFromApiAsync(double lat1, double lon1, double lat2, double lon2, NavigationMode mode, int numRoutes=3)
        {
            var apiUri = "https://atlas.microsoft.com/route/directions/json";
            string travelMode = null;
            // TODO: For now, This is only considering the Drive / Rideshare and Bus (Transit) modes
            switch (mode)
            {
                case NavigationMode.Drive:
                case NavigationMode.Rideshare:
                    travelMode = "car";
                    break;
                case NavigationMode.Transit:
                    travelMode="bus";
                    break;
                default:
                    travelMode="car";
                    break;
            }
            var queryString = $"{apiUri}?subscription-key={AzureMapsSubscriptionKey}&api-version=1.0&query={lat1},{lon1}:{lat2},{lon2}&traffic=true&routeType=fastest&maxAlternatives={numRoutes-1}&travelMode={travelMode}";
            Console.WriteLine(queryString);
            var response = await this.client.GetAsync(queryString);
            return response;
        }

        private async Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(
                System.Environment.GetEnvironmentVariable("CLIENT_ID"),
                System.Environment.GetEnvironmentVariable("CLIENT_PASSWORD")
                );
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }

        private async Task<SecretBundle> DoVault()
        {
            keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
            return await keyVaultClient.GetSecretAsync("Maps");
        }
    }
}