using Book.Domain.Entities;
namespace Book.Domain.Interfaces;

public interface IAuthorRepository
{
    Task<bool> DeleteAsync(Guid id);
    Task<bool> AddAsync(Author author);
    Task<bool> UpdateAsync(Author author);
    
    Task<IEnumerable<Author?>> GetAllAsync(bool includeDeleted=false);
    Task<Author?> GetByIdAsync(Guid id,bool includeDeleted=false);
    Task<Author?> GetByFirstNameAsync(string? firstName,bool includeDeleted=false);
    Task<Author?> GetByLastNameAsync(string? lastName,bool includeDeleted=false);
    
    Task<string?> GetFirstNameAsync(Guid id);
    Task<string?> GetLastNameAsync(Guid id);
    Task<string?> GetBioAsync(Guid id);
    Task<string?> GetPictureUrlAsync(Guid id);
    Task<DateOnly?> GetBirthDateAsync(Guid id);
    Task<DateOnly?> GetDeathDateAsync(Guid id);
    Task<string?> GetNationalityAsync(Guid id);
    
    Task<bool> SetFirstNameAsync(Guid id, string firstName);
    Task<bool> SetLastNameAsync(Guid id, string lastName);
    Task<bool> SetBioAsync(Guid id, string bio);
    Task<bool> SetPictureUrlAsync(Guid id, string pictureUrl);
    Task<bool> SetBirthDateAsync(Guid id, DateOnly birthDate);
    Task<bool> SetDeathDateAsync(Guid id, DateOnly deathDate);
    Task<bool> SetNationalityAsync(Guid id, string nationality);
}