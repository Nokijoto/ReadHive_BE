using System.Reflection;
using Application.Commands;
using Application.Interfaces;
using Application.Validators;
using Domain.Interfaces;
using FluentValidation;
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
                    typeof(RegisterCommand).Assembly 
                )
            );
        services.AddTransient<IMailService,MailService>();
        services.AddScoped<ILoggingService, SerilogLoggingService>();
        services.AddScoped<IAuthService, Application.Services.AuthService>();
        services.AddScoped<IAppUserRepository, Infrastructure.Repositories.AppUserRepository>();
        services.AddScoped<IUserService, Application.Services.UserService>();
        
        services.AddScoped<IValidator<RegisterCommand>, RegisterCommandValidator>();
        services.AddScoped<IValidator<SendResetPasswordEmailCommand>, SendResetPasswordEmailValidator>();
        services.AddScoped<IValidator<ResetPasswordCommand>, ResetPasswordCommandValidator>();      
        services.AddScoped<IValidator<ConfirmEmailCommand>, ConfirmEmailCommandValidator>();
        services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
        
        return services;
    }
    
    
}