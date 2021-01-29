using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authmanagement.Context
{
    public class ApplicationUser:IdentityUser
    {
        public DateTime DateCreated { get; set; }
        public string OriginUrl { get; set; }
        //JB. holds the cient id for which the user is created
        public string AudienceId { get; set; }
        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
