using System.Collections.Generic;

namespace Authmanagement.Proxy.clients.Dtos
{
    public class ClientResponseDto
    {
        public string Client_Id { get; set; }
        public string ClientName { get; set; }
        public string Secret { get; set; }
        public List<string> AllowedScopes { get; set; }
        public List<Dictionary<string, string>> Claims { get; set; }
    }
}
