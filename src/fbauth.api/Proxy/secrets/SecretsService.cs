using Authmanagement.Proxy.secrets.Dtos;

namespace Authmanagement.Proxy.secrets
{
    public class SecretsService : ISecretsService
    {
        private ISecretsRepository _repo;
        private ISecretsFactory _factory;
        public SecretsService(ISecretsFactory factory, ISecretsRepository repo)
        {
            _factory = factory;
            _repo = repo;
        }

        public string AddSecret(SecretBindingDto dto)
        {
            string response = "";
            if (dto.AudienceType == "ApiResource")
            {
                response = _repo.AddApiSecret(_factory.buildApiSecret(dto));
            }
            else
            {
                response = _repo.AddClientSecret(_factory.buildClientSecret(dto));
            }
            return response;
        }
    }
}
