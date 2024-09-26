using Application.Extensions;
using Application.Interfaces;
using Application.Models;
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
            _log.LogError(e.Message,e);
            throw;
        }
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        try
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentException("Email cannot be empty");
            }
            var user = await _userRepository.GetByEmailAsync(email);
            if (user != null) return await Task.FromResult(user.MapToDto());
            else return await Task.FromResult(new UserDto());
        
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<UserDto> GetByUserNameAsync(string userName)
    {
        try
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("User name cannot be empty");
            }
            var user = await _userRepository.GetByUserNameAsync(userName);
            if (user != null) return await Task.FromResult(user.MapToDto());
            else return await Task.FromResult(new UserDto()); 
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<string?> getProfilePictureUrlAsync(Guid id)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null) return user.ProfilePictureUrl;
            else return null;           
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<DateTime?> getLastLoginDateAsync(Guid id)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null) return user.LastLoginDate;
            else return null;           
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }   
    }

    public async Task<bool?> getAgreedToTermsAndConditionsAsync(Guid id)
    { 
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null) return await Task.FromResult(user.AgreedToTermsAndConditions);
            else return await Task.FromResult(false);           
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool?> getAgreedToPrivacyPolicyAsync(Guid id)
    {
      try
      {
          if(id == Guid.Empty)
          {
              throw new ArgumentException("User id cannot be empty");
          }
          var user = await _userRepository.GetByIdAsync(id);
          if (user != null) return await Task.FromResult(user.AgreedToPrivacyPolicy);
          else return await Task.FromResult(false);           
      }
      catch (Exception e)
      {
          _log.LogError(e.Message, e);
          throw;
      }
    }

    public async Task<bool?> getIsSuspendedAsync(Guid id)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null) return await Task.FromResult(user.IsSuspended);
            else return await Task.FromResult(false);           
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }   
    }

    public async Task<string?> getSuspensionReasonAsync(Guid id)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null) return await Task.FromResult(user.SuspensionReason);
            else return await Task.FromResult(String.Empty);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }   
    }

    public async Task<bool?> getIsDeletedAsync(Guid id)
    {
        try
        {
            var user = await this.GetByIdAsync(id);
            return await Task.FromResult(user.IsDeleted);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<DateTime?> getDeletedAtAsync(Guid id)
    {
        try
        {
            var user = await this.GetByIdAsync(id);
            return await Task.FromResult(user.DeletedAt);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }   
    }

    public async Task<bool?> getIsActiveAsync(Guid id)
    {
        try
        {
            var user = await this.GetByIdAsync(id);
            return await Task.FromResult(user.IsActive);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> AddAsync(UserDto userDto)
    {
        try
        {
            var user = userDto.MapToEntity();
            await _userRepository.AddAsync(user);
            return await Task.FromResult(true);
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
            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> SetFirstNameAsync(Guid id, string firstName)
    {
       try
       {
           if(id == Guid.Empty)
           {
               throw new ArgumentException("User id cannot be empty");
           }
           var user = await _userRepository.GetByIdAsync(id);
           if (user != null)
           {
               user.FirstName = firstName;
               await _userRepository.UpdateAsync(user);
           }

           return await Task.FromResult(true);
       }
       catch (Exception e)
       {
           _log.LogError(e.Message, e);
           throw;
       }
    }

    public async Task<bool> SetLastNameAsync(Guid id, string lastName)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.LastName = lastName;
                await _userRepository.UpdateAsync(user);
            }

            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> SetProfilePictureUrlAsync(Guid id, string profilePictureUrl)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.ProfilePictureUrl = profilePictureUrl;
                await _userRepository.UpdateAsync(user);
            }

            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> SetLastLoginDateAsync(Guid id, DateTime lastLoginDate)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.LastLoginDate = lastLoginDate;
                await _userRepository.UpdateAsync(user);
            }

            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> SetAgreedToTermsAndConditionsAsync(Guid id, bool agreedToTermsAndConditions)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.AgreedToTermsAndConditions = agreedToTermsAndConditions;
                await _userRepository.UpdateAsync(user);
            }

            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> SetAgreedToPrivacyPolicyAsync(Guid id, bool agreedToPrivacyPolicy)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.AgreedToPrivacyPolicy = agreedToPrivacyPolicy;
                await _userRepository.UpdateAsync(user);
            }

            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> SetIsSuspendedAsync(Guid id, bool isSuspended)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.IsSuspended = isSuspended;
                await _userRepository.UpdateAsync(user);
            }

            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> SetSuspensionReasonAsync(Guid id, string suspensionReason)
    {
        try
        {
            if(id == Guid.Empty)
            {
                throw new ArgumentException("User id cannot be empty");
            }
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                user.SuspensionReason = suspensionReason;
                await _userRepository.UpdateAsync(user);
            }

            return await Task.FromResult(true);
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> SetIsDeletedAsync(Guid id, bool isDeleted)
    {
       try
       {
           if(id == Guid.Empty)
           {
               throw new ArgumentException("User id cannot be empty");
           }
           var user = await _userRepository.GetByIdAsync(id);
           if (user != null)
           {
               user.IsDeleted = isDeleted;
               await _userRepository.UpdateAsync(user);
           }

           return await Task.FromResult(true);
       }
       catch (Exception e)
       {
           _log.LogError(e.Message, e);
           throw;
       }
    }

    public async Task<bool> SetDeletedAtAsync(Guid id, DateTime deletedAt)
    {
       try
       {
           if(id == Guid.Empty)
           {
               throw new ArgumentException("User id cannot be empty");
           }
           var user = await _userRepository.GetByIdAsync(id);
           if (user != null)
           {
               user.DeletedAt = deletedAt;
               await _userRepository.UpdateAsync(user);
           }

           return await Task.FromResult(true);
       }
       catch (Exception e)
       {
           _log.LogError(e.Message, e);
           throw;
       }
    }
    
}