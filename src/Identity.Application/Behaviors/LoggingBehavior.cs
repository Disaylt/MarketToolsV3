using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Behaviors
{
    internal class LoggingBehavior<TRequest, TResponse>(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            logger.LogInformation("Run request - {requestType}, await response - {responseType}", typeof(TRequest), typeof(TResponse));
            logger.LogDebug("Request body {body}", JsonSerializer.Serialize(request));
            
            var response = await next();

            logger.LogDebug("Response body {body}", JsonSerializer.Serialize(response));

            return response;
        }
    }
}
