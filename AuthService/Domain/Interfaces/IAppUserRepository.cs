using Domain.Entities;

namespace Domain.Interfaces;

public interface IAppUserRepository
{
    Task<AppUser> GetByIdAsync(Guid id);
    Task<AppUser> GetByEmailAsync(string email);
    Task<AppUser> GetByUserNameAsync(string userName);
    
    Task<AppUser> AddAsync(AppUser appUser);
    Task<AppUser> UpdateAsync(AppUser appUser);
    Task<bool> DeleteAsync(Guid id);    
}