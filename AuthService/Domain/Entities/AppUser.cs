using Microsoft.AspNetCore.Identity;
using ProjectBase.Interfaces;
using ProjectBase.Models;

namespace Domain.Entities;

public class AppUser : IdentityUser<Guid>, IBase
{
    
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public string? ProfilePictureUrl { get; set; }
    public DateTime? LastLoginDate { get; set; }

    public bool AgreedToTermsAndConditions { get; set; }
    public bool AgreedToPrivacyPolicy { get; set; }

    public bool IsSuspended { get; set; }
    public string? SuspensionReason { get; set; }

    public bool IsDeleted { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public new Guid Id { get; set; }
    public bool? IsActive { get; set; }
}