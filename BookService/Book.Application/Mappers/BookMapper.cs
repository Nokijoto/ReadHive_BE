using Book.Application.Models.Dto;

namespace Book.Application.Mappers;

public static class BookMapper
{
    public static BookDto ToDto(this Domain.Entities.Book book)
    {
        return new BookDto
        {
            Id = book.Id,
            Title = book.Title,
            Subtitle = book.Subtitle,            
            Isbn = book.Isbn,
            Description = book.Description,
            Author = book.Author,
            Publisher = book.Publisher,
            Language = book.Language,
            Series = book.Series,
            NumberOfPages = book.NumberOfPages,
            Dimensions = book.Dimensions,
            Format = book.Format,
            Edition = book.Edition,
            TableOfContents = book.TableOfContents,
            Price = book.Price,
            PublishedAt = book.PublishedAt,
            AuthorId = book.AuthorId,
            PublisherId = book.PublisherId,   
            GenreId = book.GenreId,
            CategoryId = book.CategoryId,
            DeletedAt = book.DeletedAt,
            CreatedAt = book.CreatedAt,
            UpdatedAt = book.UpdatedAt,
            IsActive = book.IsActive
        };
    } 
    
    
    public static Domain.Entities.Book ToEntity(this BookDto bookDto)
    {
        return new Domain.Entities.Book()
        {
            Id = bookDto.Id,
            Title = bookDto.Title,
            Subtitle = bookDto.Subtitle,            
            Isbn = bookDto.Isbn,
            Description = bookDto.Description,
            Author = bookDto.Author,
            Publisher = bookDto.Publisher,
            Language = bookDto.Language,
            Series = bookDto.Series,
            NumberOfPages = bookDto.NumberOfPages,
            Dimensions = bookDto.Dimensions,
            Format = bookDto.Format,
            Edition = bookDto.Edition,
            TableOfContents = bookDto.TableOfContents,
            Price = bookDto.Price,
            PublishedAt = bookDto.PublishedAt,
            AuthorId = bookDto.AuthorId,
            PublisherId = bookDto.PublisherId,   
            GenreId = bookDto.GenreId,
            CategoryId = bookDto.CategoryId,
            DeletedAt = bookDto.DeletedAt,
            CreatedAt = bookDto.CreatedAt,
            UpdatedAt = bookDto.UpdatedAt,
            IsActive = bookDto.IsActive
        };
    }
}