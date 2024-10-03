using Application.Models.Results;

namespace Application.Queries;

public interface IQueryHandler<in TQuery, TResponse> 
    where TQuery : IQuery<TResponse>
{
    Task<Result> Handle(TQuery query,CancellationToken cancellationToken = default);
}