using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoManagement.Database.Interfaces;
using ToDoManagement.Database.Models;
using ToDoManagement.Database.Repositories;

namespace ToDoManagement.Database;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ToDoManagementContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(nameof(ToDoManagementContext)))
        );

        services.AddScoped<IToDoTypeRepository, ToDoTypeRepository>();

        return services;
    }
}
