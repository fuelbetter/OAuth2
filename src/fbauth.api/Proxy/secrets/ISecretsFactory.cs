using Authmanagement.Context;
using Authmanagement.Proxy.resources.Dtos;
using Authmanagement.Proxy.secrets.Dtos;
using IdentityServer4.EntityFramework.Entities;

namespace Authmanagement.Proxy.secrets
{
    public interface ISecretsFactory
    {
        ClientSecret buildClientSecret(SecretBindingDto dto);
        ApiSecret buildApiSecret(SecretBindingDto dto);

        SecretBindingDto BuildApiSecretBinding(ResourceBindingDto dto, int daId, string type);
    }
}
