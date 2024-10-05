using Application.Models.Dto;
using Application.Models.Results;
using MediatR;

namespace Application.Queries.Category;

public class GetCategoryQuery : IRequest<ResultBase<CategoryDto?>>
{
    public Guid Id { get; set; }
    public GetCategoryQuery(Guid id)
    {
        Id = id;
    }
}