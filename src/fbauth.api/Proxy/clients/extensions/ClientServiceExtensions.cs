using Microsoft.Extensions.DependencyInjection;

namespace Authmanagement.Proxy.clients.extensions
{
    public static class ClientServiceExtensions
    {
        public static void ConfigureClientServices(this IServiceCollection services)
        {
            services.AddSingleton<IClientFactory, ClientFactory>();
            services.AddScoped<IClientRepository, ClientRepository>();
        }
    }
}
