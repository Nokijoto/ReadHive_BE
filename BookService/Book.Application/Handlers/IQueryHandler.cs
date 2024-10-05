using System.Threading;
using System.Threading.Tasks;
using Application.Models.Results;
using Application.Queries;

namespace Application.Handlers;

public interface IQueryHandler<in TQuery, TResponse> 
    where TQuery : IQuery<TResponse>
{
    Task<Result> Handle(TQuery query,CancellationToken cancellationToken = default);
}