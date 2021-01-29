using Microsoft.Extensions.DependencyInjection;


namespace Authmanagement.Proxy.resources.extensions
{
    public static class ResourcesServiceExtensions
    {
        public static void ConfigureResourcesServices(this IServiceCollection services)
        {
            services.AddSingleton<IResourceFactory, ResourceFactory>();
            services.AddScoped<IResourceRepository, ResourceRepository>();
        }
    }
}
