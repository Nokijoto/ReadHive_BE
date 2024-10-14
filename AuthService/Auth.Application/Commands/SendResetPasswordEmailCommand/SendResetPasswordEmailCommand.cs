using MediatR;

namespace Application.Commands;

public class SendResetPasswordEmailCommand : IRequest<bool>
{
    public string Email { get; set; }
}