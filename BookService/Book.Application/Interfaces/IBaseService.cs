using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Book.Application.Models.Dto;

namespace Book.Application.Interfaces;

public interface IBaseService<T>
{
    Task<bool> DeleteAsync(Guid id);
    Task<BookDto?> UpdateAsync(Guid id, T dto);
    Task<bool> AddAsync(T dto);
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T?>> GetAllAsync();
}