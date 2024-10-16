﻿namespace Application.Models.Dto;

public class RegisterDto
{
    public RegisterDto(string userName, string email, string password)
    {
        UserName = userName;
        Email = email;
        Password = password;
    }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
}