using MediatR;

namespace Application.Commands;

public class ConfirmEmailCommand: IRequest<bool>
{
    public string userEmail { get; set; }
    public string Token { get; set; }
}