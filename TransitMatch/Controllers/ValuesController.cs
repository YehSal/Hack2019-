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


        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            DoVault();
            return new string[] { "value1", "value2" };
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

        private static void DoVault()
        {
            kvc = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(GetToken));
            Console.WriteLine("Here");
            Console.WriteLine(kvc.GetSecretAsync("Maps"));

            // write
            // writeKeyVault();
            // Console.WriteLine("Press enter after seeing the bundle value show up");
            // Console.ReadLine();

            // SecretBundle secret = Task.Run(() => kvc.GetSecretAsync(azureMapsUrl +
            //     @"/secrets/" + "Maps")).ConfigureAwait(false).GetAwaiter().GetResult();
            // Console.WriteLine(secret.Tags["Test1"].ToString());
            // Console.WriteLine(secret.Tags["Test2"].ToString());
            // Console.WriteLine(secret.Tags["CanBeAnything"].ToString());

            // Console.ReadLine();
        }

        private static async void writeKeyVault()// string szPFX, string szCER, string szPassword)
        {
            SecretAttributes attribs = new SecretAttributes
            {
                Enabled = true//,
                              //Expires = DateTime.UtcNow.AddYears(2), // if you want to expire the info
                              //NotBefore = DateTime.UtcNow.AddDays(1) // if you want the info to 
                              // start being available later
            };

            IDictionary<string, string> alltags = new Dictionary<string, string>();
            alltags.Add("Test1", "This is a test1 value");
            alltags.Add("Test2", "This is a test2 value");
            alltags.Add("CanBeAnything", "Including a long encrypted string if you choose");
            string TestName = "TestSecret";
            string TestValue = "searchValue"; // this is what you will use to search for the item later
            string contentType = "SecretInfo"; // whatever you want to categorize it by; you name it
            SecretBundle bundle = await kvc.SetSecretAsync
               (azureMapsUrl, TestName, TestValue, alltags, contentType, attribs);
            Console.WriteLine("Bundle:" + bundle.Tags["Test1"].ToString());
        }
    }
}
