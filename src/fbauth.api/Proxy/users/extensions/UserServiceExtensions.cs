using Authmanagement.Logging;
using Authmanagement.Proxy.users.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Authmanagement.Proxy.users.extensions
{
    public static class UserServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }

        public static void ConfigureTokenService(this IServiceCollection services)
        {
            services.AddSingleton<ITokenService, TokenService>();
        }
    }
}
