using MediatR;

namespace Application.Commands.ConfirmEmail;

public class ConfirmEmailCommand: IRequest<bool>
{
    public string userEmail { get; set; }
    public string Token { get; set; }
}