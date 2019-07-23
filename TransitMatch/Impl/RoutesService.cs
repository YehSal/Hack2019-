using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AzureMapsToolkit.Common;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using TransitMatch.Models;
using TransitMatch.Services;

namespace TransitMatch.Impl
{
    public class RoutesService : IRoutesService
    {
        private readonly HttpClient client;
        private IKeyVaultClient keyVaultClient;

        public RoutesService(HttpClient client, IKeyVaultClient keyVaultClient)
        {
            client = client ?? throw new ArgumentNullException(nameof(client));
            // keyVaultClient = keyVaultClient ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<List<RouteDirectionsResult>> GetRidesAsync(double lat1, double lon1, double lat2, double lon2, NavigationMode mode)
        {
            // TODO: Factor in the modes
            // TODO: Factor in the traffic
            // TODO: Look for other query params to use
            var routes = new List<RouteDirectionsResult>();
            var response = await GetRoutes(lat1, lon1, lat2, lon2);
            var routesResponse = await response.Content.ReadAsAsync<RouteDirectionsResponse>();
            if (routesResponse != null && routesResponse.Routes.Length > 0)
            {
                routes = new List<RouteDirectionsResult>(routesResponse.Routes);
            }
            return routes;
        }

        private async Task<HttpResponseMessage> GetRoutes(double lat1, double lon1, double lat2, double lon2)
        {
            var queryString = $"https://atlas.microsoft.com/route/directions/json?subscription-key={AzureMapsSubscriptionKey}&api-version=1.0&query={lat1},{lon1}:{lat2},{lon2}";
            Console.WriteLine(queryString);
            var response = await client.GetAsync(queryString);
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