using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Application.Interfaces;
using Book.Application.Mappers;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using Book.Domain.Interfaces;
using ProjectBase.Interfaces;

namespace Book.Application.Services;

public class ShelveService : IShelveService
{
    readonly ILoggingService _log;
    readonly IShelveRepository _shelveRepository;
    public ShelveService(IShelveRepository shelveRepository, ILoggingService log)
    {
        _shelveRepository = shelveRepository;
        _log = log;
    }
    
    public async Task<bool> DeleteShelveAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Shelve id cannot be empty");
            }
            var shelve = await _shelveRepository.GetByIdAsync(id);
            if (shelve != null)
            {
                await _shelveRepository.DeleteAsync(shelve.Id);
                return true;
            }       
            return false;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<ShelveDto?>> GetShelveAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Shelve id cannot be empty");
            }
            var shelve = await _shelveRepository.GetByIdAsync(id);
            if (shelve != null)
            {
                return new ResultBase<ShelveDto?>(true,shelve.ToDto());
            }                
            return null;    
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<IEnumerable<ShelveDto?>>> GetShelvesAsync()
    {
        try
        {
            var shelves = await _shelveRepository.GetAllAsync();
            return new ResultBase<IEnumerable<ShelveDto?>>(true,shelves.Select(shelve=>shelve.ToDto()));
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<ShelveDto?>> UpdateShelveAsync(ShelveDto shelveDto)
    {
        try
        {
            if (shelveDto.Id == Guid.Empty)
            {
                throw new ArgumentException("Shelve id cannot be empty");
            }
            var shelve = await _shelveRepository.GetByIdAsync(shelveDto.Id);
            if (shelve != null)
            {   
                return new ResultBase<ShelveDto?>(true,shelve.ToDto());
            }       
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> AddShelveAsync(ShelveDto shelveDto)
    {
        try
        {
            await _shelveRepository.AddAsync(shelveDto.ToEntity());
            return true;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }
}