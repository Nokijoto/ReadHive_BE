using Book.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using ProjectBase.Models;

namespace Book.Infrastructure;

public class BookDbContext : DbContext
{
    public DbSet<Domain.Entities.Book> Books { get; set; }
    public DbSet<Author?> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Shelve> Shelves { get; set; }


    public BookDbContext(DbContextOptions<BookDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>()
            .Property(a => a.BirthDate)
            .HasConversion(
                v => v.HasValue ? (DateTime?)v.Value.ToDateTime(new TimeOnly()) : null, 
                v => v.HasValue ? DateOnly.FromDateTime(v.Value) : null); 
        modelBuilder.Entity<Author>()
            .Property(a => a.DeathDate)
            .HasConversion(
                v => v.HasValue ? (DateTime?)v.Value.ToDateTime(new TimeOnly()) : null, 
                v => v.HasValue ? DateOnly.FromDateTime(v.Value) : null);
    }
    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseModel && 
                        (e.State == EntityState.Added || 
                         e.State == EntityState.Modified || 
                         e.State == EntityState.Deleted));

        foreach (var entry in entries)
        {
            var entity = (BaseModel)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.Now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = DateTime.Now;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entity.DeletedAt = DateTime.Now;
                entry.State = EntityState.Modified;
            }
        }

        return base.SaveChanges();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is BaseModel && 
                        (e.State == EntityState.Added || 
                         e.State == EntityState.Modified || 
                         e.State == EntityState.Deleted));

        foreach (var entry in entries)
        {
            var entity = (BaseModel)entry.Entity;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedAt = DateTime.Now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entity.UpdatedAt = DateTime.Now;
            }
            else if (entry.State == EntityState.Deleted)
            {
                entity.DeletedAt = DateTime.Now;
                entry.State = EntityState.Modified;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}