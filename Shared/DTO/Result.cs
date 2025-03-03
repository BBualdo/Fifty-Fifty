namespace Shared.DTO;

public class Result<T>
{
    public bool IsSuccess { get; }
    public string Message { get; }
    public IEnumerable<string>? Errors { get; }
    public T? Data { get; }

    private Result(bool isSuccess, string message, IEnumerable<string>? errors = null, T? data = default)
    {
        IsSuccess = isSuccess;
        Message = message;
        Errors = errors;
        Data = data;
    }

    public static Result<T> Success(T data, string message) => new(true, message, null, data);

    public static Result<T> Failure(string message, IEnumerable<string> errors) => new(false, message, errors);
}