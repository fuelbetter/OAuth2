using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Infrastructure
{
    /// <summary>
    /// JB. Infrastructure service. Authenticates with Azure KeyVault and obtains the Database's connection string.
    /// </summary>
    public static class KeyvaultProxy
    {
        public static string GetUserDbConnection(string kvurl, string secretKey)
        {
            return Task.Run(async () => await GetSecretValue(kvurl+secretKey)).Result;
        }
        private static async Task<string> GetSecretValue(string source)
        {
            string Message = "";
            try
            {
                AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
                KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                var secret = await keyVaultClient.GetSecretAsync(source)
                        .ConfigureAwait(false);
                Message = secret.Value;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return Message;
        }

        public static string GetFacebookInformation(string kvurl, string secretKey)
        {
            return Task.Run(async () => await GetSecretValue(kvurl + secretKey)).Result;
        }

    }
}
