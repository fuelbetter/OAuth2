using IdentityServer4.EntityFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Context
{
    public class ApiSecret : Secret
    { 

        public int ApiResourceId { get; set; }
        public ApiResource ApiResource { get; set; }
    }
}
