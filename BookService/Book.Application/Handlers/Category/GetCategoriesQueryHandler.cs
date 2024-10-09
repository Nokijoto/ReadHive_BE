using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using Book.Application.Queries.Category;
using MediatR;

namespace Book.Application.Handlers.Category;

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, ResultBase<IEnumerable<CategoryDto?>>>
{
    private readonly ICategoryService _service;
    public GetCategoriesQueryHandler(ICategoryService service)
    {
        _service = service;
    }
    public async Task<ResultBase<IEnumerable<CategoryDto?>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetCategoriesAsync();
            return new ResultBase<IEnumerable<CategoryDto?>>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<IEnumerable<CategoryDto?>>(false, null, new List<string>() { e.Message });
        }
    }   
    
}