using Authmanagement.Proxy.clients.Dtos;
using IdentityServer4.EntityFramework.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authmanagement.Proxy.clients
{
    public interface IClientRepository
    {
        Task<object> AddClient(Client client, ClientBindingDto dto);
        ICollection<Client> Clients();
        Client UpdateClient(Client dto);
        Client FindClientById(string ClientId);
        Client FindClientByName(string ClientName);
        Task<bool> ClientExist(string ClientName);
        void RemoveClient(int Id);
    }
}
