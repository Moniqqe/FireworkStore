using Interview.FireworkStore.Core.Domain.Entity;
using Interview.FireworkStore.Core.Domain.Interfaces;
using Interview.FireworkStore.Core.Infrastructure.DataSourceProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Interview.FireworkStore.Core
{
    public static class CoreModulesExtensions
    {
        public static IServiceCollection RegisterCoreModule(this IServiceCollection services)
        {
            return services
                .AddTransient<IDataReader, DataReader>()
                .AddTransient<IDataWriter<Order>, DataWriter<Order>>();
        }
    }
}