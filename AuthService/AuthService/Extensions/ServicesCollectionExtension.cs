using Application.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Services;

namespace AuthService.Extensions;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        
        services.AddScoped<ILoggingService, SerilogLoggingService>();
        services.AddScoped<IAuthService, Application.Services.AuthService>();
        return services;
    }
    
    
}