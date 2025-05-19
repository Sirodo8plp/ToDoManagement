using Microsoft.Extensions.DependencyInjection;

namespace ToDoManagement.Cache;

public static class CacheConfiguration
{
    public static IServiceCollection AddToDoManagementCache(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<IToDoCacheService>(new ToDoCacheService(connectionString));

        return services;
    }
}
