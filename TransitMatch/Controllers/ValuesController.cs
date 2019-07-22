using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace TransitMatch.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static KeyVaultClient kvc = null;
        static string azureKeyVaultUrl = System.Environment.GetEnvironmentVariable("AZURE_KEYVAULT_URL");
        static string azureMapsSubscriptionKey = null;
        private static readonly System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<System.Net.Http.HttpResponseMessage>> Get()
        {
            azureMapsSubscriptionKey = await DoVault();
            Console.WriteLine("HERE!!");
            Console.WriteLine(azureKeyVaultUrl);
            string jsonString;
            using (System.IO.StreamReader reader = new System.IO.StreamReader(Request.Body, System.Text.Encoding.UTF8))
            {
                jsonString = await reader.ReadToEndAsync();
            }
            var stringContent = new System.Net.Http.StringContent(jsonString, System.Text.Encoding.UTF8, "application/json");

            System.Net.Http.HttpResponseMessage response = await client.PostAsync(
                $"https://atlas.microsoft.com/mapData/upload?subscription-key={azureMapsSubscriptionKey}&api-version=1.0&dataFormat=geojson",
                stringContent
                );

            // System.Net.Http.HttpResponseMessage response = await client.GetAsync(
            //     $"https://atlas.microsoft.com/route/directions/json?subscription-key={azureMapsSubscriptionKey}&api-version=1.0&query=52.50931,13.42936:52.50274,13.43872"
            // );
            Console.WriteLine($"Maps Key: {azureMapsSubscriptionKey}");
            return response;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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

        private static async Task<string> DoVault()
        {
            kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
            Console.WriteLine($"KVC: {kvc}");
            string keyVaultUri = $"{azureKeyVaultUrl}secrets/Maps";
            Microsoft.Azure.KeyVault.Models.SecretBundle secret = await kvc.GetSecretAsync(azureKeyVaultUrl, "Maps");
            Console.WriteLine("secret: " + secret.Value);
            return secret.Value != null ? secret.Value : string.Empty;

        }
    }
}
