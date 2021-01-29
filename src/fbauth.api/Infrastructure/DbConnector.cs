using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Infrastructure
{
    /// <summary>
    /// JB. Obtains the Connection String from the Azure Keyvault.
    /// Pass Azure Keyvault (url)
    /// </summary>
    public class DbConnector : IDbConnector
    {
        public async Task<string> SetUpDbContext(string source)
        {
            string Message = "";
            try
            {
                //await Task.Run(async () =>
                //{
                    AzureServiceTokenProvider azureServiceTokenProvider = new AzureServiceTokenProvider();
                    KeyVaultClient keyVaultClient = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(azureServiceTokenProvider.KeyVaultTokenCallback));
                    var secret = await keyVaultClient.GetSecretAsync(source)
                            .ConfigureAwait(false);
                    Message = secret.Value;
                //});
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return Message;
        }
    }
}
