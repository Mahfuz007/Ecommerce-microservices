using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CommonBlocks.Exceptions.Handler;

public class CustomExceptionHandler(ILogger<CustomExceptionHandler> _logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        _logger.LogError(
                "Error Message: {exceptionMessage}, Time of occurrence {time}",
                exception.Message, DateTime.UtcNow);

        var statusCode = exception switch
        {
            InternalServerException => context.Response.StatusCode = StatusCodes.Status500InternalServerError,
            NotFoundException => context.Response.StatusCode = StatusCodes.Status404NotFound,
            BadRequestException => context.Response.StatusCode = StatusCodes.Status400BadRequest,
            ValidationException => context.Response.StatusCode = StatusCodes.Status400BadRequest,
            _ => context.Response.StatusCode = StatusCodes.Status500InternalServerError
        };

        var problemDetails = new ProblemDetails
        {
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Status = statusCode,
            Instance = context.Request.Path
        };

        problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

        if(exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);
        }

        await context.Response.WriteAsJsonAsync( problemDetails, cancellationToken: cancellationToken);

        return true;
    }
}
