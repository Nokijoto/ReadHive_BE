using Application.Extensions;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Interfaces;

namespace Application.Services;

public class UserService: IUserService
{
    private readonly ILoggingService _log;
    private readonly IAppUserRepository _userRepository;
    
    public UserService(ILoggingService log, IAppUserRepository userRepository)
    {
        _log = log;
        _userRepository = userRepository;
    }
    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null) return await Task.FromResult(user.MapToDto());
            else return await Task.FromResult(new UserDto());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<UserDto> GetByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }

    public Task<UserDto> GetByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<string?> getProfilePictureUrlAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<DateTime?> getLastLoginDateAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> getAgreedToTermsAndConditionsAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> getAgreedToPrivacyPolicyAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> getIsSuspendedAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<string?> getSuspensionReasonAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> getIsDeletedAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<DateTime?> getDeletedAtAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool?> getIsActiveAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddAsync(UserDto userDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(UserDto userDto)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetFirstNameAsync(Guid id, string firstName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetLastNameAsync(Guid id, string lastName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetProfilePictureUrlAsync(Guid id, string profilePictureUrl)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetLastLoginDateAsync(Guid id, DateTime lastLoginDate)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetAgreedToTermsAndConditionsAsync(Guid id, bool agreedToTermsAndConditions)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetAgreedToPrivacyPolicyAsync(Guid id, bool agreedToPrivacyPolicy)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetIsSuspendedAsync(Guid id, bool isSuspended)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetSuspensionReasonAsync(Guid id, string suspensionReason)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetIsDeletedAsync(Guid id, bool isDeleted)
    {
        throw new NotImplementedException();
    }

    public Task<bool> SetDeletedAtAsync(Guid id, DateTime deletedAt)
    {
        throw new NotImplementedException();
    }
    
}