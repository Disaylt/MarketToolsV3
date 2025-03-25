﻿using Identity.Domain.Seed;
using Microsoft.AspNetCore.Diagnostics;

namespace Identity.WebApi.ExceptionHandlers
{
    public class RootExceptionHandler(IProblemDetailsService problemDetailsService)
    : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not RootServiceException rootServiceException)
            {
                return false;
            }

            httpContext.Response.StatusCode = (int)rootServiceException.StatusCode;

            var problemDetailsContext = new ProblemDetailsContext()
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails =
                {
                    Status = (int)rootServiceException.StatusCode,
                    Title = "Ошибка обработки данных.",
                    Detail = "Приложение не смогло обработать ваш запрос. Пожалуйста исправьте ошибки которые вы видете и попробуйте снова."
                }
            };

            Dictionary<string, IEnumerable<string>> errorMessages = new()
            {
                { "Messages", rootServiceException.Messages }
            };

            problemDetailsContext.ProblemDetails.Extensions.TryAdd("errors", errorMessages);

            return await problemDetailsService.TryWriteAsync(problemDetailsContext);
        }
    }
}
