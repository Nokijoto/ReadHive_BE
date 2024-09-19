namespace Application.Models;

public class UserDto 
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? UserName { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public string? Email { get; set; }

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
    
    public string? NormalizedEmail{ get; set; }
    public string? NormalizeUserName{ get; set; }
    
    public bool EmailConfirmed { get; set; }
    public string? PhoneNumber { get; set; }
    public bool? PhoneNumberConfirmed { get; set; }
    public bool? TwoFactorEnabled { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public bool? LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
}