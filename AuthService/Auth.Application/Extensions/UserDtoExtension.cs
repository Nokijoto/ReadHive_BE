using Application.Models.Dto;
using Domain.Entities;

namespace Application.Extensions;

public static class UserDtoExtension
{
    public static AppUser MapToEntity(this UserDto userDto)
    {
        return new AppUser
        {
            Id = userDto.Id,
            Email = userDto.Email,
            UserName = userDto.UserName,
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            ProfilePictureUrl = userDto.ProfilePictureUrl,
            LastLoginDate = userDto.LastLoginDate,
            AgreedToTermsAndConditions = userDto.AgreedToTermsAndConditions,
            AgreedToPrivacyPolicy = userDto.AgreedToPrivacyPolicy,
            IsSuspended = userDto.IsSuspended,
            SuspensionReason = userDto.SuspensionReason,
            IsDeleted = userDto.IsDeleted,
            DeletedAt = userDto.DeletedAt,
            IsActive = userDto.IsActive
        };  
    }
    
}