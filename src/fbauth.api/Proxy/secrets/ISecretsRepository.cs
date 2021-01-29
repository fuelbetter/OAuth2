using Authmanagement.Context;
using IdentityServer4.EntityFramework.Entities;

namespace Authmanagement.Proxy.secrets
{
    public interface ISecretsRepository
    {
        string AddClientSecret(ClientSecret model);
        string AddApiSecret(ApiSecret model);
    }
}
