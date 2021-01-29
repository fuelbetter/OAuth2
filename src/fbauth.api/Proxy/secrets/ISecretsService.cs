

using Authmanagement.Proxy.secrets.Dtos;

namespace Authmanagement.Proxy.secrets
{
    public interface ISecretsService
    {
        string AddSecret(SecretBindingDto dto);
    }
}
