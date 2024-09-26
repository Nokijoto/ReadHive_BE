using Application.Commands;
using Application.Models;
using AuthService.Models.Requests;
using AutoMapper;

namespace AuthService.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Domain.Entities.AppUser, UserDto>().ReverseMap();
        CreateMap<RegisterRequest,RegisterCommand>().ReverseMap();
    }
}