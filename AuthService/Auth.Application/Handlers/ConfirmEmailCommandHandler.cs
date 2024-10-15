using Application.Commands;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Handlers;

public class ConfirmEmailCommandHandler : IRequestHandler<ConfirmEmailCommand, bool>
{
    private readonly UserManager<AppUser> _userManager;

    public ConfirmEmailCommandHandler(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.userEmail);
        if (user == null) return false;

        var result = await _userManager.ConfirmEmailAsync(user, Uri.UnescapeDataString(request.Token));
        return result.Succeeded;
    }
}