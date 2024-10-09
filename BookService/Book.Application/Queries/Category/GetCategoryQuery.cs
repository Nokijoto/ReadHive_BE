using System;
using Book.Application.Models.Dto;
using Book.Application.Models.Results;
using MediatR;

namespace Book.Application.Queries.Category;

public class GetCategoryQuery : IRequest<ResultBase<CategoryDto?>>
{
    public Guid Id { get; set; }
    public GetCategoryQuery(Guid id)
    {
        Id = id;
    }
}