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

    public async Task<AppUser?> GetByIdAsync(Guid id)
    {
        try
        {
            return await _context.AppUsers.SingleOrDefaultAsync(u => u.Id == id);
        }
        catch (Exception e)
        {
            _log.LogError("Error getting user by id", e);
            throw;
        }
    }

    public async Task<AppUser?> GetByEmailAsync(string email)
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

    public async Task<AppUser?> GetByUserNameAsync(string userName)
    {
        try
        {
            return await _context.AppUsers.SingleOrDefaultAsync(u => u.UserName == userName);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<string?> getProfilePictureUrlAsync(Guid id)
    {
        try
        {
            var user=GetByIdAsync(id);
            return await Task.FromResult(user.Result?.ProfilePictureUrl);
        }
        catch (Exception e)
        {
            _log.LogError("Error getting profile picture url", e);
            throw;
        }
    }

    public async Task<DateTime?> getLastLoginDateAsync(Guid id)
    {
        try
        {
            var user=GetByIdAsync(id);
            return await Task.FromResult(user.Result?.LastLoginDate);
        }
        catch (Exception e)
        {
            _log.LogError("Error getting last login date", e);
            throw;
        }
    }

    public async Task<bool?> getAgreedToTermsAndConditionsAsync(Guid id)
    {
        try
        {
            var user=GetByIdAsync(id);
            return await Task.FromResult(user.Result?.AgreedToTermsAndConditions);
        }
        catch (Exception e)
        {
            _log.LogError("Error getting agreed to terms and conditions", e);
            throw;
        }
    }

    public async Task<bool?> getAgreedToPrivacyPolicyAsync(Guid id)
    {
        try
        {
            var user=GetByIdAsync(id);
            return await Task.FromResult(user.Result?.AgreedToPrivacyPolicy);
        }
        catch (Exception e)
        {
            _log.LogError("Error getting agreed to privacy policy", e);
            throw;
        }
    }

    public async Task<bool?> getIsSuspendedAsync(Guid id)
    {
        try
        {
            var user=GetByIdAsync(id);
            return await Task.FromResult(user.Result?.IsSuspended);
        }
        catch (Exception e)
        {
            _log.LogError("Error getting is suspended", e);
            throw;
        }
    }

    public async Task<string?> getSuspensionReasonAsync(Guid id)
    {
        try
        {
            var user=GetByIdAsync(id);
            return await Task.FromResult(user.Result?.SuspensionReason);
        }
        catch (Exception e)
        {
            _log.LogError("Error getting suspension reason", e);
            throw;
        }
    }

    public async Task<bool?> getIsDeletedAsync(Guid id)
    {
        try
        {
            var user=GetByIdAsync(id);
            return await Task.FromResult(user.Result?.IsDeleted);
        }
        catch (Exception e)
        {
            _log.LogError("Error getting is deleted", e);
            throw;
        }
    }

    public async Task<DateTime?> getDeletedAtAsync(Guid id)
    {
        try
        {
            var user=GetByIdAsync(id);
            return await Task.FromResult(user.Result?.DeletedAt);
        }
        catch (Exception e)
        {
            _log.LogError("Error getting deleted at", e);
            throw;
        }   
    }

    public async Task<bool?> getIsActiveAsync(Guid id)
    {
        try
        {
            var user=GetByIdAsync(id);
            return await Task.FromResult(user.Result?.IsActive);
        }
        catch (Exception e)
        {
            _log.LogError("Error getting is active", e);
            throw;
        }   
    }

    public async Task<bool> AddAsync(AppUser appUser)
    {
        try
        {
            await _context.AppUsers.AddAsync(appUser);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error adding user", e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(AppUser appUser)
    {
        try
        {
            _context.AppUsers.Update(appUser);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error updating user", e);
            throw;
        }
    }

    public async Task<bool> SetFirstNameAsync(Guid id, string firstName)
    {
        try
        {
            var user=GetByIdAsync(id);
            user.Result!.FirstName=firstName;
            await UpdateAsync(user.Result);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error setting first name", e);
            throw;
        }
    }

    public async Task<bool> SetLastNameAsync(Guid id, string lastName)
    {
        try
        {
            var user=GetByIdAsync(id);
            user.Result!.LastName=lastName;
            await UpdateAsync(user.Result);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error setting last name", e);
            throw;
        }   
    }

    public async Task<bool> SetProfilePictureUrlAsync(Guid id, string profilePictureUrl)
    {
        try
        {
            var user=GetByIdAsync(id);
            user.Result!.ProfilePictureUrl=profilePictureUrl;
            await UpdateAsync(user.Result);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error setting profile picture url", e);
            throw;
        }
    }

    public async Task<bool> SetLastLoginDateAsync(Guid id, DateTime lastLoginDate)
    {
        try
        {
            var user=GetByIdAsync(id);
            user.Result!.LastLoginDate=lastLoginDate;
            await UpdateAsync(user.Result);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error setting last login date", e);
            throw;
        }
    }

    public async Task<bool> SetAgreedToTermsAndConditionsAsync(Guid id, bool agreedToTermsAndConditions)
    {
        try
        {
            var user=GetByIdAsync(id);
            user.Result!.AgreedToTermsAndConditions=agreedToTermsAndConditions;
            await UpdateAsync(user.Result);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error setting agreed to terms and conditions", e);
            throw;
        }   
    }

    public async Task<bool> SetAgreedToPrivacyPolicyAsync(Guid id, bool agreedToPrivacyPolicy)
    {
        try
        {
            var user=GetByIdAsync(id);
            user.Result!.AgreedToPrivacyPolicy=agreedToPrivacyPolicy;
            await UpdateAsync(user.Result);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error setting agreed to privacy policy", e);
            throw;
        }   
    }

    public async Task<bool> SetIsSuspendedAsync(Guid id, bool isSuspended)
    {
        try
        {
            var user=GetByIdAsync(id);
            user.Result!.IsSuspended=isSuspended;
            await UpdateAsync(user.Result);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error setting is suspended", e);
            throw;
        }
    }

    public async Task<bool> SetSuspensionReasonAsync(Guid id, string suspensionReason)
    {
        try
        {
            var user=GetByIdAsync(id);
            user.Result!.SuspensionReason=suspensionReason;
            await UpdateAsync(user.Result);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error setting suspension reason", e);
            throw;
        }   
    }

    public async Task<bool> SetIsDeletedAsync(Guid id, bool isDeleted)
    {
        try
        {
            var user=GetByIdAsync(id);
            user.Result!.IsDeleted=isDeleted;
            await UpdateAsync(user.Result);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error setting is deleted", e);
            throw;
        }
    }

    public async Task<bool> SetDeletedAtAsync(Guid id, DateTime deletedAt)
    {
        try
        {
            var user=GetByIdAsync(id);
            user.Result!.DeletedAt=deletedAt;
            await UpdateAsync(user.Result);
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error setting deleted at", e);
            throw;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var user=GetByIdAsync(id);
            _context.AppUsers.Remove(user.Result!);
            await _context.SaveChangesAsync();
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError("Error deleting user", e);
            throw;
        }
    }


}