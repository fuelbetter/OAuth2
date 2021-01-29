using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Infrastructure
{
    public interface IDbConnector
    {
        Task<string> SetUpDbContext(string source);
    }
}
