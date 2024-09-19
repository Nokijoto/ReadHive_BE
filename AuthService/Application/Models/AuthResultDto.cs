using System;
using System.Collections.Generic;

namespace Application.Models;

public class AuthenticationResultDto
{
    public bool Succeeded { get; set; }
    public string Token { get; set; } // Token JWT
    public IEnumerable<Exception> Errors { get; set; }
    public DateTime? Expiration { get; set; }
}