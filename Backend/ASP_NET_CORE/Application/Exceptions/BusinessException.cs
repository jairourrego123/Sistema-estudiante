
namespace Application.Exceptions;

public class BusinessException: AppException
{
    public BusinessException(string message) : base(message, System.Net.HttpStatusCode.BadRequest) { }

}
