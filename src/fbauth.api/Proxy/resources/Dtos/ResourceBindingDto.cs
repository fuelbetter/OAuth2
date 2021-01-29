using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Proxy.resources.Dtos
{
    public class ResourceBindingDto
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }

        public string Secret { get; set; }
    }
}
