using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CommonBlocks.Behavior;

public class LoggingBehavior<TReqeust, TResponse> 
    (ILogger<LoggingBehavior<TReqeust, TResponse>> _logger)
    : IPipelineBehavior<TReqeust, TResponse>
    where TReqeust : notnull, IRequest<TResponse>
    where TResponse: notnull
{
    public async Task<TResponse> Handle(TReqeust request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _logger.
            LogInformation($"[Start] Handle Request={typeof(TReqeust).Name} - Response={typeof(TResponse).Name} - RequestData={request}");

        var timer = new Stopwatch();
        timer.Start();

        var response = await next();

        timer.Stop();
        var timeTaken = timer.Elapsed;

        if(timeTaken.Seconds > 3)
        {
            _logger.LogWarning($"[Performance] This Request={typeof(TReqeust).Name} took {timeTaken.Seconds} seconds");
        }

        _logger.LogInformation($"[END] Handled Request={typeof(TReqeust).Name} with Response={response}");
        return response;
    }
}
