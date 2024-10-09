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

public class GetCategoryQueryHandler : IRequestHandler<GetCategoryQuery, ResultBase<CategoryDto?>>
{
    private readonly ICategoryService _service;
    public GetCategoryQueryHandler(ICategoryService service)
    {
        _service = service;
    }
    public async Task<ResultBase<CategoryDto?>> Handle(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.GetCategoryAsync(request.Id);
            return new ResultBase<CategoryDto?>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<CategoryDto?>(false, null, new List<string>() { e.Message });
        }
    }   
}