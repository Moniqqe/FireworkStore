using Interview.FireworkStore.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Interview.FireworkStore.DI
{
    public static class RegisterModulesExtensions
    {
        public static IServiceCollection RegisterModules(this IServiceCollection services)
        {
            return services
                .AddSingleton<IUserService, UserService>()
                .AddSingleton<IFireworkService, FireworkService>()
                .AddSingleton<IOrderService, OrderService>();
        }
    }
}
