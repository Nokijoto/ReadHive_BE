using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class BookDbContext : DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Shelve> Shelves { get; set; }


    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }
      
}