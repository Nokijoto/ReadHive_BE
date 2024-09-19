﻿using Domain.Entities;

namespace Domain.Interfaces;

public interface IAppUserRepository
{
    Task<AppUser?> GetByIdAsync(Guid id);
    Task<AppUser?> GetByEmailAsync(string email);
    Task<AppUser?> GetByUserNameAsync(string userName);
    
    Task<string?> getProfilePictureUrlAsync(Guid id);
    Task<DateTime?> getLastLoginDateAsync(Guid id);
    Task<bool?> getAgreedToTermsAndConditionsAsync(Guid id);
    Task<bool?> getAgreedToPrivacyPolicyAsync(Guid id);
    Task<bool?> getIsSuspendedAsync(Guid id);
    Task<string?> getSuspensionReasonAsync(Guid id);
    Task<bool?> getIsDeletedAsync(Guid id);
    Task<DateTime?> getDeletedAtAsync(Guid id);
    Task<bool?> getIsActiveAsync(Guid id);
    
    Task<bool> AddAsync(AppUser appUser);
    Task<bool> UpdateAsync(AppUser appUser);
    
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