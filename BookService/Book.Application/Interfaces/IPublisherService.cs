using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;

namespace Book.Application.Interfaces;

public interface IPublisherService
{
    Task<bool> DeletePublisherAsync(Guid id);
    Task<ResultBase<PublisherDto?>> GetPublisherAsync(Guid id);
    Task<ResultBase<IEnumerable<PublisherDto?>>> GetPublishersAsync();
    Task<ResultBase<PublisherDto?>> UpdatePublisherAsync(PublisherDto publisherDto);
    Task<bool> AddPublisherAsync(PublisherDto publisherDto);
}