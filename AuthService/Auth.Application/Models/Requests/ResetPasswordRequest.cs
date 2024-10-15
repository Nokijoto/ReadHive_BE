namespace Application.Models.Requests;

public class ResetPasswordRequest
{
    public string email;
    public string token;
    public string newPassword;
}