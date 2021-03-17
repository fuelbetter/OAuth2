using Authmanagement.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace Authmanagement.Proxy.users.extensions
{
    public static class UserServiceExtensions
    {
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}
