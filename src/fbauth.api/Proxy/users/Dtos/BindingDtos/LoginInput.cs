using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Proxy.users.Dtos.BindingDtos
{
    public class LoginInput
    {
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
