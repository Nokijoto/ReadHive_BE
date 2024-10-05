using System.Collections.Generic;
using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.Category;

public class GetCategoriesQuery : IRequest<ResultBase<IEnumerable<CategoryDto?>>>
{
    public GetCategoriesQuery()
    {
        
    }
}