using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Proxy.secrets.Dtos
{
    public class SecretBindingDto
    {
        public string Description { get; set; }
        public string value { get; set; }//JB. To be Hashed

        public int ResourceId { get; set; }
        /// <summary>
        /// Client_Id for building a CLientSecret or ApiResource name for an ApiResourceSecret.
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// Tell me please what type are you? i.e. Client or ApiResource?
        /// </summary>
        public string AudienceType { get; set; }
    }
}
