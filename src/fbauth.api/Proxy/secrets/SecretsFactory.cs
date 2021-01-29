using System;
using Authmanagement.Context;
using Authmanagement.Proxy.resources.Dtos;
using Authmanagement.Proxy.secrets.Dtos;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.Models;

namespace Authmanagement.Proxy.secrets
{
    public class SecretsFactory: ISecretsFactory
    {
        public ClientSecret buildClientSecret(SecretBindingDto dto)
        {

            return new ClientSecret
            {

                ClientId = dto.ResourceId,
                Type = "SharedSecret",
                Value = dto.value.Sha256(),
                Created = DateTime.Now,
                Description = dto.Description
            };
        }
        public ApiSecret buildApiSecret(SecretBindingDto dto)
        {

            return new ApiSecret
            {
                ApiResourceId = dto.ResourceId,
                Type = "SharedSecret",
                Value = dto.value.Sha256(),
                Created = DateTime.Now,
                Description = dto.Description
            };
        }

        public SecretBindingDto BuildApiSecretBinding(ResourceBindingDto dto, int daId, string type)
        {
            return new SecretBindingDto
            {
                ResourceId = daId,
                value = dto.Secret,
                Audience = dto.Name,
                AudienceType = type,
                Description = "Secrets for " + dto.Name
            };
        }
    }
}
