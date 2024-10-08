namespace Book.Application.Commands;

public interface ICommand : IBaseCommand
{
    
}

public interface ICommand<TResponse> : IBaseCommand
{
}

public interface IBaseCommand
{
    
}