using Application.Models.Dto;
using MediatR;

namespace Application.Queries.GetMe;

public class GetUserQuery : IRequest<UserDto>
{
    public Guid UserId { get; }

    public GetUserQuery(Guid userId)
    {
        UserId = userId;
    }
}