using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class AppUserRepository : IAppUserRepository
{
    private readonly AppDbContext _context;
    private readonly ILoggingService _log;

    public AppUserRepository(AppDbContext context, ILoggingService log)
    {
        _context = context;
        _log = log;
    }

    public Task<AppUser> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<AppUser> GetByEmailAsync(string email)
    {
        try
        {
            return await _context.AppUsers.SingleOrDefaultAsync(u => u.Email == email);
        }
        catch (Exception e)
        {
            _log.LogError("Error getting user by email", e);
            throw;
        }
    }

    public Task<AppUser> GetByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    Task<AppUser> IAppUserRepository.AddAsync(AppUser appUser)
    {
        throw new NotImplementedException();
    }

    public Task<AppUser> UpdateAsync(AppUser appUser)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task AddAsync(AppUser user)
    {
        await _context.AppUsers.AddAsync(user);
        await _context.SaveChangesAsync();
    }

}