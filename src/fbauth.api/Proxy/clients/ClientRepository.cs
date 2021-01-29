using Authmanagement.Context;
using Authmanagement.Proxy.clients.Dtos;
using Authmanagement.Proxy.helpers;
using IdentityServer4.EntityFramework.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Authmanagement.Proxy.clients
{
    public class ClientRepository : IClientRepository
    {
        private IClientFactory _factory;
        private ApplicationDbContext _ctx;

        public ClientRepository(IClientFactory factory, ApplicationDbContext context)
        {
            _factory = factory;
            _ctx = context;
        }
        public async Task<object> AddClient(Client client, ClientBindingDto dto)
        {
            //JB. Build a newly randomdized Secret, this is what is passed to the client and it is not hashed yet. It will be hashed at persisting time.
            string NewSecret = await RandomStringGenerator.GeneratedString();

            Object response;
            int clientId = 0;
            try
            {
                using (_ctx)
                {
                    //JB. Create the client.
                    _ctx.Clients.Add(client);
                    _ctx.SaveChanges();
                    clientId = client.Id;
                    //JB. Add now the Secret
                    await _ctx.ClientSecrets.AddAsync(_factory.CreateClientSecret(clientId, NewSecret));

                    //JB. Add Client Grant Type
                    await _ctx.ClientGrantTypes.AddAsync(new ClientGrantType { ClientId = clientId, GrantType = IdentityServer4.Models.GrantType.ClientCredentials });
                    //JB. Add Scopes this client is allowed in the system.
                    //foreach (var scopev in dto.AllowedScopes)
                    //{
                    //    await _ctx.ClientScopes.AddAsync(new ClientScope { ClientId = clientId, Scope = scopev });
                    //}
                    ////JB. Add claims. Info about this Client
                    //foreach (var c in dto.Claims)
                    //{
                    //    await _ctx.ClientClaims.AddAsync(new ClientClaim { ClientId = clientId, Type = c["Type"], Value = c["Value"] });
                    //}
                    _ctx.SaveChanges();

                };

                response = new ClientResponseDto
                {
                    ClientName = client.ClientName,
                    Client_Id = client.ClientId,
                    Secret = NewSecret,
                    AllowedScopes = dto.AllowedScopes,
                    Claims = dto.Claims
                };

            }
            catch (Exception ex)
            {
                response = new ClientErrorResponseDto { Error = HttpStatusCode.InternalServerError.ToString(), Message = ex.Message };
            }

            return response;
        }

        public async Task<bool> ClientExist(string ClientName)
        {
            bool clientExist = false;
            var result = await _ctx.Clients.Where(i => i.ClientName == ClientName).FirstOrDefaultAsync();
            //string  clientName  = result.ClientName;
            if (result != null)
            {
                clientExist = true;
            }

            return clientExist;
        }

        public ICollection<Client> Clients()
        {
            throw new NotImplementedException();
        }

        public Client FindClientById(string ClientId)
        {
            throw new NotImplementedException();
        }

        public Client FindClientByName(string ClientName)
        {
            throw new NotImplementedException();
        }

        public void RemoveClient(int Id)
        {
            throw new NotImplementedException();
        }

        public Client UpdateClient(Client dto)
        {
            throw new NotImplementedException();
        }
    }
}
