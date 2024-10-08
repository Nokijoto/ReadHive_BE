using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands.Category;
using Book.Application.Interfaces;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Handlers.Category;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ResultBase<bool>>
{
    private readonly ICategoryService _service;
    public DeleteCategoryCommandHandler(ICategoryService service)
    {
        _service = service;
    }
    public async Task<ResultBase<bool>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _service.DeleteCategoryAsync(request.Id);
            if(result)
            {
                return new ResultBase<bool>( result, result);
            }
            return new ResultBase<bool>(false, false, new List<string>() { "Cate not found" });
        }   
        catch (Exception e)
        {
            return new ResultBase<bool>(false, false, new List<string>() { e.Message });
        }
    }
    
}