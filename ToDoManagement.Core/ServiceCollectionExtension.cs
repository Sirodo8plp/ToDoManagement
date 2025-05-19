using Microsoft.Extensions.DependencyInjection;
using ToDoManagement.Core.Interfaces;
using ToDoManagement.Core.Services;

namespace ToDoManagement.Core;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddToDoManagementServices(this IServiceCollection services)
    {
        return services.AddScoped<IToDoTypeService, ToDoTypeService>();
    }
}
