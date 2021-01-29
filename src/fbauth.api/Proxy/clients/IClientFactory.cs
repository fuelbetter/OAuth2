using Authmanagement.Proxy.clients.Dtos;
using IdentityServer4.EntityFramework.Entities;

namespace Authmanagement.Proxy.clients
{
    public interface IClientFactory
    {
        Client CreateClientEntity(ClientBindingDto dto);
        ClientSecret CreateClientSecret(int clientId, string secret);
    }
}
