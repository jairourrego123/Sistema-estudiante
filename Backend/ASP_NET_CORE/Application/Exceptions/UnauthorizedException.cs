

namespace Application.Exceptions;

public class UnauthorizedException : AppException
{
    public UnauthorizedException(string message) : base(message, System.Net.HttpStatusCode.Unauthorized) {}
}
