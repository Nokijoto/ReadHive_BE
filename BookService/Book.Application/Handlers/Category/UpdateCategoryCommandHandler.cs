using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Book;
using Book.Application.Commands.Category;
using Book.Application.Interfaces;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Handlers.Category;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ResultBase<CategoryDto?>>
{
    private readonly ICategoryService _service;
    public UpdateCategoryCommandHandler(ICategoryService service)
    {
        _service = service;
    }
    public async Task<ResultBase<CategoryDto?>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.UpdateCategoryAsync(request.Category);
            return new ResultBase<CategoryDto?>(true, result.Data);
        }
        catch (Exception e)
        {
            return new ResultBase<CategoryDto?>(false, null, new List<string>() { e.Message });
        }
    }   
    
}