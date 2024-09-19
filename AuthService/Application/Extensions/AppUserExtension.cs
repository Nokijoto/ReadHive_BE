using Application.Models;
using Domain.Entities;

namespace Application.Extensions;

public static class AppUserExtension
{
    public static UserDto MapToDto(this AppUser user)
    {
        return new UserDto
        {
            Id = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            ProfilePictureUrl = user.ProfilePictureUrl,
            LastLoginDate = user.LastLoginDate,
            AgreedToTermsAndConditions = user.AgreedToTermsAndConditions,
            AgreedToPrivacyPolicy = user.AgreedToPrivacyPolicy,
            IsSuspended = user.IsSuspended,
            SuspensionReason = user.SuspensionReason,
            IsDeleted = user.IsDeleted,
            DeletedAt = user.DeletedAt,
            IsActive = user.IsActive
        };
    }   
}