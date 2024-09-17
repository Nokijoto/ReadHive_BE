﻿using System.ComponentModel.DataAnnotations;

namespace AuthService.Models.Requests;

public class RegisterRequest
{
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public string UserName { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 6)]
    public string Password { get; set; }

    // // Dodatkowe pola
    // [Required]
    // [StringLength(50)]
    // public string FirstName { get; set; }
    //
    // [Required]
    // [StringLength(50)]
    // public string LastName { get; set; }
}