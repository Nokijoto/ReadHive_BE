using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.Models.Dto;
using Domain.Entities;
using Domain.Interfaces;
using Book.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services;

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

    public async Task<PublisherDto?> GetPublisherAsync(Guid id)
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
                var publisherDto = new PublisherDto()
                {
                    Id = publisher.Id,
                    Name = publisher.Name,
                    DeletedAt = publisher.DeletedAt,
                    CreatedAt = publisher.CreatedAt,
                    UpdatedAt = publisher.UpdatedAt,
                };
                return publisherDto;
            }                
            return null;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<IEnumerable<PublisherDto?>> GetPublishersAsync()
    {
        try
        {
            var publishers = await _publisherRepository.GetAllAsync();
            return new List<PublisherDto?>(publishers.Select(publisher => publisher != null ? new PublisherDto() : new PublisherDto()));
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }

    public async Task<PublisherDto?> UpdatePublisherAsync(PublisherDto publisherDto)
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
                publisher.Name = publisherDto.Name;
                await _publisherRepository.UpdateAsync(publisher);
                return publisherDto;
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
            var publisher = new Publisher()
            {
                Name = publisherDto.Name,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };      
            await _publisherRepository.AddAsync(publisher);
            return true;
        }
        catch (Exception e)
        {
            _log.LogError(e.Message, e);
            throw;
        }
    }
}