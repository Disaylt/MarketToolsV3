using MarketToolV3.Authentication.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace MarketToolV3.Authentication.Services.Implementation;

internal class AuthExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not AuthException)
        {
            return false;
        }

        var problemDetailsContext = new ProblemDetailsContext()
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails =
            {
                Status = 401,
                Title = "Ошибка аутентификации.",
                Detail = "Не удалось получить данные пользователя."
            }
        };

        return await problemDetailsService.TryWriteAsync(problemDetailsContext);
    }
}