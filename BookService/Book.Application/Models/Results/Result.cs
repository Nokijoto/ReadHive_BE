namespace Book.Application.Models.Results;

public class Result
{
    
}

public class Result<T> : Result
{
    public T? Data { get; set; }

    public Result(T data)
    {
        Data = data;
    }
}