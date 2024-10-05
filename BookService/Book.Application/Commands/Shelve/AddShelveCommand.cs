using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Commands.Shelve;

public class AddShelveCommand : IRequest<ResultBase<ShelveDto?>>
{
    public ShelveDto? ShelveDto { get; set; }
    
    public AddShelveCommand(ShelveDto shelveDto)
    {
        ShelveDto = shelveDto;
    }
}