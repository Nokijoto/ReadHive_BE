using Application.Commands;
using Application.Models.Dto;
using Application.Models.Requests;
using AutoMapper;

namespace Application.Mapping;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Domain.Entities.AppUser, UserDto>().ReverseMap();
        CreateMap<RegisterRequest,RegisterCommand>().ReverseMap();
    }
}