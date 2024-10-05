using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Book.Infrastructure.Interfaces;

namespace Application.Services;

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

    public async Task<ShelveDto?> GetShelveAsync(Guid id)
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
                var shelveDto = new ShelveDto()
                {
                    Id = shelve.Id,
                    Description = shelve.Description,
                    DeletedAt = shelve.DeletedAt,
                    CreatedAt = shelve.CreatedAt,
                    UpdatedAt = shelve.UpdatedAt,
                    Title = shelve.Title,
                    OwnerId = shelve.OwnerId,
                };
                return shelveDto;
            }                
            return null;    
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<IEnumerable<ShelveDto?>> GetShelvesAsync()
    {
        try
        {
            var shelves = await _shelveRepository.GetAllAsync();
            return new List<ShelveDto?>(shelves.Select(shelve => shelve != null ? new ShelveDto() : new ShelveDto()));
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ShelveDto?> UpdateShelveAsync(ShelveDto shelveDto)
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
                shelve.Description = shelveDto.Description;
                shelve.Title = shelveDto.Title;
                shelve.OwnerId = shelveDto.OwnerId;
                await _shelveRepository.UpdateAsync(shelve);
                return shelveDto;
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
            var shelve = new Shelve()
            {
                Description = shelveDto.Description,
                Title = shelveDto.Title,
                OwnerId = shelveDto.OwnerId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };      
            await _shelveRepository.AddAsync(shelve);
            return true;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }
}