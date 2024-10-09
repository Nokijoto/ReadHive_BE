using System.Threading;
using System.Threading.Tasks;
using Book.Application.Queries;
using Book.Application.Models.Results;

namespace Application.Handlers;

public interface IQueryHandler<in TQuery, TResponse> 
    where TQuery : IQuery<TResponse>
{
    Task<Result> Handle(TQuery query,CancellationToken cancellationToken = default);
}