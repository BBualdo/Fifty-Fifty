namespace DTOs;

public class ValidationResult
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public List<string>? ErrorList { get; set; }

    public ValidationResult()
    {
        IsSuccess = true;
    }

    public ValidationResult(string message) {
        IsSuccess = true;
        Message = message;
    }

    public ValidationResult(bool isSuccess, string message)
    {
        IsSuccess = isSuccess;
        Message = message;
    }

    public ValidationResult(string message, IEnumerable<string> errors)
    {
        IsSuccess = false;
        Message = message;
        ErrorList = [.. errors];
    }
}