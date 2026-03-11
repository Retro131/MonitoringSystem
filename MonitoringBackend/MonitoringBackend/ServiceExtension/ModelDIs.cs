using InfotecsBackend.Repository;
using InfotecsBackend.Repository.Interfaces;
using InfotecsBackend.Service;

#pragma warning disable CS1591

namespace InfotecsBackend.ServiceExtension;

public static class ModelDIs
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ISessionRepository, SessionRepository>();
        services.AddScoped<IDeviceRepository, DeviceRepository>();
        
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDeviceSessionService, DeviceSessionService>();
        
        return services;
    }
}