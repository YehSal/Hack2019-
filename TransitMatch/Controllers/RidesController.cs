using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using AzureMapsToolkit.Common;

namespace TransitMatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RidesController : ControllerBase
    {
        static KeyVaultClient kvc = null;
        static string azureMapsUrl = System.Environment.GetEnvironmentVariable("AZURE_MAPS_URL");
        static Microsoft.Azure.KeyVault.Models.SecretBundle azureMapsSecret = null;
        private static readonly System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();
        private readonly string AzureMapsSubscriptionKey = "yXcIJq3mB7bC-mdkyuKytBmnHGLHpVK2ewYL0sFJUbQ";

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<System.Net.Http.HttpResponseMessage>> Get()
        {
            azureMapsSecret = await DoVault();
            System.Net.Http.HttpResponseMessage response = await client.GetAsync(
                $"https://atlas.microsoft.com/mapData/upload?subscription-key={AzureMapsSubscriptionKey}&api-version=1.0&dataFormat=geojson"
                );
            return response;
        }

        // GET api/rides/rideRequest
        // &query=52.50931,13.42936:52.50274,13.43872
        //https://localhost:5001/api/rides/requestRide?lat1=6&lon1=6&lat2=10&lon2=20
        //52.50931,13.42936:52.50274,13.43872
        [HttpGet("requestRide")]
        public async Task<ActionResult<string>> Get(double lat1, double lon1, double lat2, double lon2, int newNum)
        {
            var num = newNum;
            var client = new HttpClient();
            var queryString = $"https://atlas.microsoft.com/route/directions/json?subscription-key={AzureMapsSubscriptionKey}&api-version=1.0&query={lat1},{lon1}:{lat2},{lon2}";
            Console.WriteLine(queryString);
            var response = await client.GetAsync(queryString);
            var responseString = await response.Content.ReadAsStringAsync();
            var deserializedObject = await response.Content.ReadAsAsync<RouteDirectionsResponse>();
            Console.WriteLine(deserializedObject);
            Console.WriteLine(deserializedObject.FormatVersion);
            return responseString;
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
