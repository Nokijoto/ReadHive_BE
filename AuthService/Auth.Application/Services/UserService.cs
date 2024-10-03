using Application.Extensions;
using Application.Interfaces;
using Application.Models.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Interfaces;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly ILoggingService _log;
    private readonly IAppUserRepository _userRepository;

    public UserService(ILoggingService log, IAppUserRepository userRepository)
    {
        _log = log;
        _userRepository = userRepository;
    }

    private async Task<T?> GetUserFieldAsync<T>(Guid id, Func<AppUser, T> selector)
    {
        ValidateUserId(id);
        var user = await _userRepository.GetByIdAsync(id);
        return user != null ? selector(user) : default;
    }

    private void ValidateUserId(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("User id cannot be empty");
        }
    }

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        try
        {
            ValidateUserId(id);
            var user = await _userRepository.GetByIdAsync(id);
            return user?.MapToDto() ?? new UserDto();
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        ValidateEmail(email);
        try
        {
            var user = await _userRepository.GetByEmailAsync(email);
            return user?.MapToDto() ?? new UserDto();
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<UserDto> GetByUserNameAsync(string userName)
    {
        ValidateUserName(userName);
        try
        {
            var user = await _userRepository.GetByUserNameAsync(userName);
            return user?.MapToDto() ?? new UserDto();
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<string?> GetProfilePictureUrlAsync(Guid id)
    {
        return await GetUserFieldAsync(id, user => user.ProfilePictureUrl);
    }

    public async Task<DateTime?> GetLastLoginDateAsync(Guid id)
    {
        return await GetUserFieldAsync(id, user => user.LastLoginDate);
    }

    public async Task<bool?> GetAgreedToTermsAndConditionsAsync(Guid id)
    {
        return await GetUserFieldAsync(id, user => user.AgreedToTermsAndConditions);
    }

    public async Task<bool?> GetAgreedToPrivacyPolicyAsync(Guid id)
    {
        return await GetUserFieldAsync(id, user => user.AgreedToPrivacyPolicy);
    }

    public async Task<bool?> GetIsSuspendedAsync(Guid id)
    {
        return await GetUserFieldAsync(id, user => user.IsSuspended);
    }

    public async Task<string?> GetSuspensionReasonAsync(Guid id)
    {
        return await GetUserFieldAsync(id, user => user.SuspensionReason);
    }

    public async Task<bool?> GetIsDeletedAsync(Guid id)
    {
        var user = await GetByIdAsync(id);
        return user.IsDeleted;
    }

    public async Task<DateTime?> GetDeletedAtAsync(Guid id)
    {
        var user = await GetByIdAsync(id);
        return user.DeletedAt;
    }

    public async Task<bool?> GetIsActiveAsync(Guid id)
    {
        var user = await GetByIdAsync(id);
        return user.IsActive;
    }

    public async Task<bool> AddAsync(UserDto userDto)
    {
        try
        {
            var user = userDto.MapToEntity();
            await _userRepository.AddAsync(user);
            return true;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(UserDto userDto)
    {
        try
        {
            var user = userDto.MapToEntity();
            await _userRepository.UpdateAsync(user);
            return true;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> SetFirstNameAsync(Guid id, string firstName)
    {
        return await SetUserFieldAsync(id, user => user.FirstName = firstName);
    }

    public async Task<bool> SetLastNameAsync(Guid id, string lastName)
    {
        return await SetUserFieldAsync(id, user => user.LastName = lastName);
    }

    public async Task<bool> SetProfilePictureUrlAsync(Guid id, string profilePictureUrl)
    {
        return await SetUserFieldAsync(id, user => user.ProfilePictureUrl = profilePictureUrl);
    }

    public async Task<bool> SetLastLoginDateAsync(Guid id, DateTime lastLoginDate)
    {
        return await SetUserFieldAsync(id, user => user.LastLoginDate = lastLoginDate);
    }

    public async Task<bool> SetAgreedToTermsAndConditionsAsync(Guid id, bool agreedToTermsAndConditions)
    {
        return await SetUserFieldAsync(id, user => user.AgreedToTermsAndConditions = agreedToTermsAndConditions);
    }

    public async Task<bool> SetAgreedToPrivacyPolicyAsync(Guid id, bool agreedToPrivacyPolicy)
    {
        return await SetUserFieldAsync(id, user => user.AgreedToPrivacyPolicy = agreedToPrivacyPolicy);
    }

    public async Task<bool> SetIsSuspendedAsync(Guid id, bool isSuspended)
    {
        return await SetUserFieldAsync(id, user => user.IsSuspended = isSuspended);
    }

    public async Task<bool> SetSuspensionReasonAsync(Guid id, string suspensionReason)
    {
        return await SetUserFieldAsync(id, user => user.SuspensionReason = suspensionReason);
    }

    public async Task<bool> SetIsDeletedAsync(Guid id, bool isDeleted)
    {
        return await SetUserFieldAsync(id, user => user.IsDeleted = isDeleted);
    }

    public async Task<bool> SetDeletedAtAsync(Guid id, DateTime deletedAt)
    {
        return await SetUserFieldAsync(id, user => user.DeletedAt = deletedAt);
    }

    private async Task<bool> SetUserFieldAsync(Guid id, Action<AppUser> setter)
    {
        ValidateUserId(id);
        var user = await _userRepository.GetByIdAsync(id);
        if (user != null)
        {
            setter(user);
            await _userRepository.UpdateAsync(user);
            return true;
        }
        return false;
    }

    private void ValidateEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException("Email cannot be empty");
        }
    }

    private void ValidateUserName(string userName)
    {
        if (string.IsNullOrEmpty(userName))
        {
            throw new ArgumentException("User name cannot be empty");
        }
    }
    
}