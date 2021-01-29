// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Authmanagement.Extensions;
using Authmanagement.Infrastructure;
using Authmanagement.Proxy.clients.extensions;
using Authmanagement.Proxy.users.extensions;
using Authmanagement.Proxy.secrets.extensions;
using Authmanagement.Proxy.resources.extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            var connString = KeyvaultProxy.GetUserDbConnection(keyVaultUrl);

            services.AddControllers();

            //JB. Below, each domain in the Proxy has its own ServiceConfiguration for DI.
            services.ConfigureClientServices();
            services.ConfigureLoggerService();
            services.ConfigureTokenService();
            services.ConfigureCustomIdentity();
            services.ConfigureIdentityServer(connString);
            services.ConfigureFuelbetterUsersDb(connString);
            services.ConfigureSecretsServices();
            services.ConfigureResourcesServices();

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

            // uncomment, if you want to add MVC
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
