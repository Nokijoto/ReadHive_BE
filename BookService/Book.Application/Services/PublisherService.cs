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

public class PublisherService :IPublisherService
{
    private readonly ILoggingService _log;
    private readonly IPublisherRepository _publisherRepository;
    
    public PublisherService(ILoggingService log, IPublisherRepository publisherRepository)
    {
        _log = log;
        _publisherRepository = publisherRepository;
    }
    public async Task<bool> DeletePublisherAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Publisher id cannot be empty");
            }
            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher != null)
            {
                await _publisherRepository.DeleteAsync(publisher.Id);
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

    public async Task<ResultBase<PublisherDto?>> GetPublisherAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Publisher id cannot be empty");
            }
            var publisher = await _publisherRepository.GetByIdAsync(id);
            if (publisher != null)
            {
                return new ResultBase<PublisherDto?>(true , publisher.ToDto());
            }                
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<IEnumerable<PublisherDto?>>> GetPublishersAsync()
    {
        try
        {
            var publishers = await _publisherRepository.GetAllAsync();
            return new ResultBase<IEnumerable<PublisherDto?>>(true, publishers.Select(publisher => publisher.ToDto()));
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<ResultBase<PublisherDto?>> UpdatePublisherAsync(PublisherDto publisherDto)
    {
        try
        {
            if (publisherDto.Id == Guid.Empty)
            {
                throw new ArgumentException("Publisher id cannot be empty");
            }
            var publisher = await _publisherRepository.GetByIdAsync(publisherDto.Id);
            if (publisher != null)
            {
                return new ResultBase<PublisherDto?>(true, publisher.ToDto());
            }       
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<bool> AddPublisherAsync(PublisherDto publisherDto)
    {
        try
        {      
            await _publisherRepository.AddAsync(publisherDto.ToEntity());
            return true;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }
}