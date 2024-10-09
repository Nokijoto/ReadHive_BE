using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Category;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Handlers.Category;

public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand,ResultBase<CategoryDto?>>
{
    private readonly ICategoryService _service;
    public AddCategoryCommandHandler(ICategoryService service)
    {
        _service = service;
    }
    public async Task<ResultBase<CategoryDto?>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.AddCategoryAsync(request.CategoryDto);
            return new ResultBase<CategoryDto?>(true, request.CategoryDto);
        }
        catch (Exception e)
        {
            return new ResultBase<CategoryDto?>(false, null, new List<string>() { e.Message });
        }
    }   
}