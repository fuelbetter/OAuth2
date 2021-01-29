

using Microsoft.Extensions.DependencyInjection;

namespace Authmanagement.Proxy.secrets.extensions
{
    public static class SecretsServiceExtensions
    {
        public static void ConfigureSecretsServices(this IServiceCollection services)
        {
            services.AddSingleton<ISecretsFactory, SecretsFactory>();
            services.AddScoped<ISecretsRepository, SecretsRepository>();
            services.AddScoped<ISecretsService, SecretsService>();
        }
    }
}
