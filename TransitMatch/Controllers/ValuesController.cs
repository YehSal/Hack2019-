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
        static string azureMapsUrl = System.Environment.GetEnvironmentVariable("AZURE_MAPS_URL");
        static Microsoft.Azure.KeyVault.Models.SecretBundle azureMapsSecret = null;
        private static readonly System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<System.Net.Http.HttpResponseMessage>> Get()
        {
            azureMapsSecret = await DoVault();
            System.Net.Http.HttpResponseMessage response = await client.GetAsync(
                $"https://atlas.microsoft.com/mapData/upload?subscription-key={azureMapsSecret}&api-version=1.0&dataFormat=geojson"
                );
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

        private static async Task<SecretBundle> DoVault()
        {
            kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
            return await kvc.GetSecretAsync("Maps");
        }
    }
}
