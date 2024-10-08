using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;

namespace Book.Application.Interfaces;

public interface IShelveService
{
    Task<bool> DeleteShelveAsync(Guid id);
    Task<ResultBase<ShelveDto?>> GetShelveAsync(Guid id);
    Task<ResultBase<IEnumerable<ShelveDto?>>> GetShelvesAsync();
    Task<ResultBase<ShelveDto?>> UpdateShelveAsync(ShelveDto shelveDto);
    Task<bool> AddShelveAsync(ShelveDto shelveDto);
}