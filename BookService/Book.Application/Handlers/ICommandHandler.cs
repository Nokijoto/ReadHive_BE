using System.Threading;
using System.Threading.Tasks;
using Book.Application.Commands;
using Book.Application.Models.Results;

namespace Application.Handlers;

public interface ICommandHandler<in TCommand> 
    where TCommand : ICommand    
{
    Task<Result> Handle(TCommand command, CancellationToken cancellationToken = default);
}

public interface ICommandHandler<in TCommand, TResponse> 
    where TCommand : ICommand<TResponse>
{
    Task<Result<TResponse>> Handle(TCommand command, CancellationToken cancellationToken = default);
}