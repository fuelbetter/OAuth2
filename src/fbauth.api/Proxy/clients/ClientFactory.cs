using Authmanagement.Proxy.clients.Dtos;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;
using System;

namespace Authmanagement.Proxy.clients
{
    public class ClientFactory : IClientFactory
    {
        public IdentityServer4.EntityFramework.Entities.Client CreateClientEntity(ClientBindingDto dto)
        {
            return new IdentityServer4.EntityFramework.Entities.Client
            {
                ClientName = dto.ClientName,
                Description = dto.Description,
                ClientUri = dto.ClientUri,
                LogoUri = dto.LogoUri,
                Created = DateTime.Today,
                Enabled = true,
                ClientId = Guid.NewGuid().ToString("N"),
            };
        }

        /// <summary>
        /// Create a new Client Secret
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="secret"></param>
        /// <returns></returns>
        public ClientSecret CreateClientSecret(int clientId, string secret)
        {
            return new ClientSecret
            {
                ClientId = clientId,
                Type = "SharedSecret",
                Description = "Secret for client " + clientId,
                Value = secret.Sha256(),
                Created = DateTime.Now

            };
        }
    }
}
