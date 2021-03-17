using Authmanagement.Extensions;
using Authmanagement.Infrastructure;
using Authmanagement.Proxy.clients.extensions;
using Authmanagement.Proxy.users.extensions;
using Authmanagement.Proxy.secrets.extensions;
using Authmanagement.Proxy.resources.extensions;
using Authmanagement.Proxy.scopes.extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Authmanagement.Configuration;
using Authmanagement.Proxy.tokens.extensions;

namespace fbauth.api
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }
        public IConfiguration _config { get; }
        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            _config = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            //JB. Prepare and onbtain DB conn string from Azure KeyVault.
            var keyVaultUrl = _config.GetConnectionString("ConnectionSource");
            var connString = KeyvaultProxy.GetUserDbConnection(keyVaultUrl, "UsersDbConnString");
            var facebookAppId = _config.GetSection("KeyvaultSecretKeys:facebookappId");
            var facebookAppKey = _config.GetSection("KeyvaultSecretKeys:facebookappKey");

            services.AddControllers();

            //JB. Below, each domain in the Proxy has its own ServiceConfiguration for DI.
            services.ConfigureClientServices();
            services.ConfigureLoggerService();
            services.ConfigureCustomIdentity();
            services.ConfigureIdentityServer(connString);
            services.ConfigureFuelbetterUsersDb(connString);
            services.ConfigureSecretsServices();
            services.ConfigureResourcesServices();
            services.ConfigureScopeServices();
            services.ConfigureTokensServices();
            //Configure Facebook, all secret info coming from Azure Keyvault.
            services.AddAuthentication().AddFacebook(facebookOptions => {
                facebookOptions.AppId = KeyvaultProxy.GetFacebookInformation(keyVaultUrl, facebookAppId.Value);
                facebookOptions.AppSecret = KeyvaultProxy.GetFacebookInformation(keyVaultUrl, facebookAppKey.Value);
                facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
            });
        }
        public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var fordwardedHeaderOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto | ForwardedHeaders.XForwardedHost
            };
            fordwardedHeaderOptions.KnownNetworks.Clear();
            fordwardedHeaderOptions.KnownProxies.Clear();

            app.UseStaticFiles();
            app.UseRouting();
            
            app.UseIdentityServer();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
