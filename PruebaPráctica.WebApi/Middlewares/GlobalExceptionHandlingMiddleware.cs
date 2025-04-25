using System.Diagnostics;
using System.Text.Json;
using PruebaPráctica.Application.Common.Exceptions;

namespace PruebaPráctica.WebApi.Middlewares;

public class GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
{
    private record ExceptionDetails(int StatusCode, object Message);
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var exceptionDetails = new ExceptionDetails(
                StatusCode: StatusCodes.Status500InternalServerError,
                Message: $"[Exception: {ex.Message}] [Inner Exception: {ex.InnerException?.Message}]"
            );
            
            switch (ex)
            {
                case ValidationException validationEx:
                    exceptionDetails  = new ExceptionDetails(
                        StatusCode: StatusCodes.Status400BadRequest,
                        Message: validationEx.Errors
                    );
                    break;
                
                case BadRequestException:
                    exceptionDetails = new ExceptionDetails(
                        StatusCode: StatusCodes.Status400BadRequest,
                        Message: ex.Message
                    );
                    break;
                
                case NotFoundException:
                    exceptionDetails = new ExceptionDetails(
                        StatusCode: StatusCodes.Status404NotFound,
                        Message: ex.Message
                    );
                    break;
                
                default:
                    logger.LogCritical(ex, "Exception: {Exception} InnerException: {InnerException}",
                        ex.Message, ex.InnerException?.Message);
                    break;
            }
            
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = exceptionDetails.StatusCode;

            string exceptionResult = JsonSerializer.Serialize(exceptionDetails, new JsonSerializerOptions {PropertyNameCaseInsensitive = true});
            await context.Response.WriteAsync(exceptionResult);
        }
    }
}