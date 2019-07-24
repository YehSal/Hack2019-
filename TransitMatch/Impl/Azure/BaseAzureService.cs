using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.KeyVault;
using Microsoft.Azure.KeyVault.Models;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

namespace TransitMatch.Impl.Azure
{
    public abstract class BaseAzureService
    {
        private static readonly string AzureKeyVaultUrl = System.Environment.GetEnvironmentVariable("AZURE_KEYVAULT_URL");

        private readonly HttpClient _httpClient;
        private KeyVaultClientFactory _kcClientFactory;

        public BaseAzureService(KeyVaultClientFactory kcClientFactory)
        {
            _kcClientFactory = kcClientFactory;
            _httpClient = new HttpClient();
        }

        protected abstract String SecretResourcePath();

        protected async Task<HttpResponseMessage> GetAzureResource(String resourceId, String resourcePath, String query)
        {
            var azureUrl = await GetAzureUrl(resourceId, resourcePath, query);
            Console.WriteLine("Getting from URL");
            Console.WriteLine(azureUrl);
            return await _httpClient.GetAsync(azureUrl);
        }

        private async Task<string> GetAzureUrl(string resourceId, string resourcePath, string query)
        {
            Console.WriteLine("Getting access token");
            var accessSecret = await GetAccessTokenForResource(SecretResourcePath());
            var azureUrl =
                $"https://{resourceId}.microsoft.com/{resourcePath}?subscription-key={accessSecret}&api-version=1.0&{query}";
            return azureUrl;
        }

        protected async Task<HttpResponseMessage> PostAzureResource(String resourceId, String resourcePath, String query, JsonResult body)
        {
            var azureUrl = await GetAzureUrl(resourceId, resourcePath, query);
            return await _httpClient.PostAsync(azureUrl,
                new StringContent(body.ToString(), Encoding.UTF8, "application/json"));
        }
        private async Task<String> GetAccessTokenForResource(String secretResource)
        {
            var kvClient = await _kcClientFactory.GetKeyVaultClient();
            Console.WriteLine("Got kv client");
            String keyVaultUri = $"{AzureKeyVaultUrl}secrets/Maps";
            var secretBundle = await kvClient.GetSecretAsync(AzureKeyVaultUrl, secretResource);
            return secretBundle.Value ?? String.Empty;
        }
    }
}
