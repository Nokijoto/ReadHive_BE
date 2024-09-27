﻿using Application.Models.Dto;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserDto> GetByIdAsync(Guid id);
    Task<UserDto> GetByEmailAsync(string email);
    Task<UserDto> GetByUserNameAsync(string userName);
    
    Task<string?> GetProfilePictureUrlAsync(Guid id);
    Task<DateTime?> GetLastLoginDateAsync(Guid id);
    Task<bool?> GetAgreedToTermsAndConditionsAsync(Guid id);
    Task<bool?> GetAgreedToPrivacyPolicyAsync(Guid id);
    Task<bool?> GetIsSuspendedAsync(Guid id);
    Task<string?> GetSuspensionReasonAsync(Guid id);
    Task<bool?> GetIsDeletedAsync(Guid id);
    Task<DateTime?> GetDeletedAtAsync(Guid id);
    Task<bool?> GetIsActiveAsync(Guid id);
    
    Task<bool> AddAsync(UserDto userDto);
    Task<bool> UpdateAsync(UserDto userDto);
    
    Task<bool> SetFirstNameAsync(Guid id, string firstName);
    Task<bool> SetLastNameAsync(Guid id, string lastName);
    Task<bool> SetProfilePictureUrlAsync(Guid id, string profilePictureUrl);
    Task<bool> SetLastLoginDateAsync(Guid id, DateTime lastLoginDate);
    
    Task<bool> SetAgreedToTermsAndConditionsAsync(Guid id, bool agreedToTermsAndConditions);
    Task<bool> SetAgreedToPrivacyPolicyAsync(Guid id, bool agreedToPrivacyPolicy);
    
    Task<bool> SetIsSuspendedAsync(Guid id, bool isSuspended);
    Task<bool> SetSuspensionReasonAsync(Guid id, string suspensionReason);
    Task<bool> SetIsDeletedAsync(Guid id, bool isDeleted);
    Task<bool> SetDeletedAtAsync(Guid id, DateTime deletedAt);  
}

