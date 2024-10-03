using Application.Interfaces;
using Book.Infrastructure.Interfaces;
using Book.Infrastructure.Services;
using Domain.Interfaces;
using Book.Infrastructure.Repositories;
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
                cfg.RegisterServicesFromAssemblies(typeof(Application.Commands.Author.AddAuthorCommand).Assembly));

            // Rejestracja serwisów
            services.AddScoped<IAuthorService, Application.Services.AuthorService>();
            services.AddScoped<IBookService, Application.Services.BookService>();
            services.AddScoped<IShelveService, Application.Services.ShelveService>();
            services.AddScoped<IPublisherService, Application.Services.PublisherService>();
            services.AddScoped<IGenreService, Application.Services.GenreService>();
            services.AddScoped<ICategoryService, Application.Services.CategoryService>();

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

