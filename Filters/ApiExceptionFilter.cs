using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace APICatalogo.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    private readonly ILogger<ApiExceptionFilter> _logger;

    public ApiExceptionFilter(ILogger<ApiExceptionFilter> logger)
    {
        _logger = logger;
    }
    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Ocorreu um erro inesperado: {Message}", context.Exception.Message);
        context.Result = new ObjectResult(new
            {
                Message = "Ocorreu um erro inesperado. Por favor, tente novamente mais tarde."
            })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
    }
}
