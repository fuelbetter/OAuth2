using Authmanagement.Context;
using Authmanagement.Infrastructure;
using fbauth.api;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Authmanagement.Extensions
{
    public static class ServiceExtensions
    {
       // public static 
        
        public static void ConfigureCustomIdentity(this IServiceCollection services)
        {

            var identity = services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Password.RequiredLength = 6;
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });
            identity.AddEntityFrameworkStores<ApplicationDbContext>();
            identity.AddDefaultTokenProviders();
            identity.AddClaimsPrincipalFactory <UserClaimsPrincipalFactory<ApplicationUser>>();
            

        }
        public static void ConfigureIdentityServer(this IServiceCollection services, string connString)
        {
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            //JB. Below, IdentityServer and DbContyext Service Configurations.
            var builder = services.AddIdentityServer(options =>
            {
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;

            })
                .AddAspNetIdentity<ApplicationUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = b => b.UseSqlServer(connString,
                    sql => sql.MigrationsAssembly(migrationsAssembly));
                });
            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();
        }

        public static void ConfigureFuelbetterUsersDb(this IServiceCollection services, string connString)
        {
            //JB. Configure usersDb
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connString));
        }
    }
}
