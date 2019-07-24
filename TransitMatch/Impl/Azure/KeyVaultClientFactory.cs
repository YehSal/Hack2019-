using System;
using System.Threading.Tasks;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;

namespace TransitMatch.Impl.Azure
{
    public class KeyVaultClientFactory
    {
        private KeyVaultClient _keyVaultClient;

        public async Task<KeyVaultClient> GetKeyVaultClient()
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            //if (_keyVaultClient == null)
            //    _keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
            //return _keyVaultClient;
            return _keyVaultClient ?? (_keyVaultClient = await Task.Run(async () =>
            {
                Console.WriteLine("Generating KV Client");
                var t = new TaskCompletionSource<string>();
                KeyVaultClient kvc = new KeyVaultClient(async (authority, resource, scope) =>
                {
                    Console.WriteLine("Got authority, resource, scope");
                    var authContext = new AuthenticationContext(authority);
                    ClientCredential clientCred = new ClientCredential(
                        System.Environment.GetEnvironmentVariable("CLIENT_ID"),
                        System.Environment.GetEnvironmentVariable("CLIENT_PASSWORD")
                    );
                    AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

                    if (result == null)
                        throw new InvalidOperationException("Failed to obtain the JWT token");
                    t.TrySetResult(result.AccessToken);
                    return result.AccessToken;
                });
                // await t.Task;
                return kvc;
            }));
        }
    }
}