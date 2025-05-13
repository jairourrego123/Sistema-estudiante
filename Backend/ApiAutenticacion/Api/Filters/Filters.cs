using Application.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<GlobalExceptionFilter> _logger;

        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var exception = context.Exception;

            _logger.LogError(exception, "Ocurrió una excepción.");

            if (exception is AppException appEx)
            {
                context.Result = new ObjectResult(new { message = appEx.Message })
                {
                    StatusCode = (int)appEx.StatusCode
                };
            }
            else
            {
                context.Result = new ObjectResult(new { message = "Ha ocurrido un error inesperado." })
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
