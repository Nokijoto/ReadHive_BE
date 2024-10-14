using System.Reflection;
using Application.Interfaces;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using Infrastructure.Services;

namespace AuthService.Extensions;

public static class ServicesCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(Program));
        services.AddMediatR(
            cfg => 
                cfg.RegisterServicesFromAssemblies(
                    typeof(Application.Commands.Register.RegisterCommand).Assembly 
                )
            );
        services.AddTransient<IMailService,MailService>();
        services.AddScoped<ILoggingService, SerilogLoggingService>();
        services.AddScoped<IAuthService, Application.Services.AuthService>();
        services.AddScoped<IAppUserRepository, Infrastructure.Repositories.AppUserRepository>();
        services.AddScoped<IUserService, Application.Services.UserService>();
        
        return services;
    }
    
    
}