using Identity.Application.Queries;
using Identity.WebApi.Models;
using MediatR;

namespace Identity.WebApi.Services;

public class SessionStateService(IMediator mediator)
    : ISessionStateService
{
    public async Task<SessionValidStatusDto> GetSessionValidStatus(string? refreshToken, CancellationToken cancellationToken = default)
    {
        SessionValidStatusDto result = new();

        if (string.IsNullOrEmpty(refreshToken))
        {
            return result;
        }

        CheckSessionActiveStatusQuery query = new CheckSessionActiveStatusQuery
        {
            RefreshToken = refreshToken
        };

        result.IsValid = await mediator.Send(query, cancellationToken);

        return result;
    }
}