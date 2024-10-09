using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Commands.Shelve;

public class AddShelveCommand : IRequest<ResultBase<ShelveDto?>>
{
    public ShelveDto? ShelveDto { get; set; }
    
    public AddShelveCommand(ShelveDto shelveDto)
    {
        ShelveDto = shelveDto;
    }
}