using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Proxy.clients.Dtos
{
    public class ClientBindingDto
    {
        [Required]
        public string ClientName { get; set; }
        public string Description { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }

        public List<string> AllowedScopes { get; set; }
        public List<Dictionary<string, string>> Claims { get; set; }
    }
}
