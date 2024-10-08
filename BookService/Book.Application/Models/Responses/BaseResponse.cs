namespace Book.Application.Models.Responses;

public class BaseResponse<T>
{

    // public string? Status { get; set; }
    public T? Data { get; set; }

}