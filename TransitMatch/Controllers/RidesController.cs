using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using AzureMapsToolkit.Common;
using System.Collections.Generic;
using TransitMatch.Models;

namespace TransitMatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RidesController : ControllerBase
    {
        private static HttpClient client = new HttpClient();
        static KeyVaultClient kvc = null;
        static string azureMapsUrl = System.Environment.GetEnvironmentVariable("AZURE_MAPS_URL");
        static Microsoft.Azure.KeyVault.Models.SecretBundle azureMapsSecret = null;
        private readonly string AzureMapsSubscriptionKey = "yXcIJq3mB7bC-mdkyuKytBmnHGLHpVK2ewYL0sFJUbQ";

        // GET api/rides
        // https://localhost:5001/api/rides/requestRide?lat1=52.50931&lon1=13.42936&lat2=52.50274&lon2=13.43872
        [HttpGet]
        public async Task<ActionResult<List<RouteDirectionsResult>>> Get(double lat1, double lon1, double lat2, double lon2, NavigationMode mode)
        {
            // TODO: Factor in the modes
            var routes = new List<RouteDirectionsResult>();
            var response = await GetRoutes(lat1, lon1, lat2, lon2);
            var routesResponse = await response.Content.ReadAsAsync<RouteDirectionsResponse>();
            if (routesResponse != null && routesResponse.Routes.Length > 0)
            {
                routes = new List<RouteDirectionsResult>(routesResponse.Routes);
            }
            return routes;
        }

        public async Task<HttpResponseMessage> GetRoutes(double lat1, double lon1, double lat2, double lon2)
        {
            var queryString = $"https://atlas.microsoft.com/route/directions/json?subscription-key={AzureMapsSubscriptionKey}&api-version=1.0&query={lat1},{lon1}:{lat2},{lon2}";
            Console.WriteLine(queryString);
            var response = await client.GetAsync(queryString);
            return response;
        }

        public static async Task<string> GetToken(string authority, string resource, string scope)
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

        private static async Task<SecretBundle> DoVault()
        {
            kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
            return await kvc.GetSecretAsync("Maps");
        }
    }
}
