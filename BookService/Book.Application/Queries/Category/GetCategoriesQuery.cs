using System.Collections.Generic;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.Category;

public class GetCategoriesQuery : IRequest<ResultBase<IEnumerable<CategoryDto?>>>
{
    public GetCategoriesQuery()
    {
        
    }
}