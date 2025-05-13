namespace Domain.Exceptions;

public class BaseDomainException : Exception
{
    public int StatusCode { get; }

    public BaseDomainException(string message, int statusCode = 400) : base(message)
    {
        StatusCode = statusCode;
    }
}
