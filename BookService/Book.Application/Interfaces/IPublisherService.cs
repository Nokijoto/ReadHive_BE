using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Models.Dto;

namespace Application.Interfaces;

public interface IPublisherService
{
    Task<bool> DeletePublisherAsync(Guid id);
    Task<PublisherDto?> GetPublisherAsync(Guid id);
    Task<IEnumerable<PublisherDto?>> GetPublishersAsync();
    Task<PublisherDto?> UpdatePublisherAsync(PublisherDto publisherDto);
    Task<bool> AddPublisherAsync(PublisherDto publisherDto);
}