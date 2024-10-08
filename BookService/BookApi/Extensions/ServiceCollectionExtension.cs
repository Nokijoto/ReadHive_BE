using Book.Application.Interfaces;
using Book.Application.Commands.Author;
using Book.Application.Services;
using Book.Domain.Interfaces;
using Book.Infrastructure.Repositories;
using ProjectBase.Interfaces;
using ProjectBase.Services;
using Serilog;

namespace BookApi.Extensions
{
    public static class ServicesCollectionExtension
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Rejestracja Serilog jako singleton
            services.AddSingleton<Serilog.ILogger>(provider => Log.Logger);
    
            // Rejestracja ILoggingService
            services.AddScoped<ILoggingService, SerilogLoggingService>();

            // Rejestracja MediatR
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssemblies(typeof(AddAuthorCommand).Assembly));

            // Rejestracja serwisów
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IShelveService, ShelveService>();
            services.AddScoped<IPublisherService, PublisherService>();
            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<ICategoryService, CategoryService>();

            // Rejestracja repozytoriów
            services.AddScoped<IAuthorRepository, AuthorRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IShelveRepository, ShelveRepository>(); 
            services.AddScoped<IPublisherRepository, PublisherRepository>();
            services.AddScoped<IGenreRepository, GenreRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>(); 
        
            return services;
        }
    
    
    }
}

