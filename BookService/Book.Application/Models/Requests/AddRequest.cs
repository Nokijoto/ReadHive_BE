namespace Book.Application.Models.Requests;

public class AddRequest<T>
{
    public AddRequest() {}

    public T? Data { get; set; }
}