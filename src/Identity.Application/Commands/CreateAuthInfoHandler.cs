using Identity.Application.Models;
using Identity.Application.Services.Abstract;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands
{
    public class CreateAuthInfoHandler(IRepository<Session> sessionRepository,
        ILogger<CreateAuthInfoHandler> logger,
        ITokenService<JwtAccessTokenDto> accessTokenService,
        ITokenService<JwtRefreshTokenDto> refreshTokenService,
        ISessionService sessionService,
        IProviderClaimsService providerClaimsService)
        : IRequestHandler<CreateAuthInfo, AuthInfoDto>
    {
        public async Task<AuthInfoDto> Handle(CreateAuthInfo request, CancellationToken cancellationToken)
        {
            if (await refreshTokenService.IsValid(request.RefreshToken) == false)
            {
                logger.LogWarning("Refresh token isn't valid");

                return new AuthInfoDto { IsValid = false };
            }

            JwtRefreshTokenDto refreshTokenData = refreshTokenService.Read(request.RefreshToken);

            Session session = await sessionRepository.FindByIdRequiredAsync(refreshTokenData.Id, cancellationToken);

            if (session.IsActive == false || session.Token != request.RefreshToken)
            {
                logger.LogWarning("Session status not active ({status}) or current refresh token does not match session refresh token.", session.IsActive);

                return new AuthInfoDto { IsValid = false };
            }

            string refreshToken = refreshTokenService.Create(refreshTokenData);

            await sessionService.UpdateAsync(session, refreshToken, request.UserAgent, cancellationToken);

            JwtAccessTokenDto accessTokenData = CreateAccessTokenData(session.IdentityId, session.Id);
            accessTokenData.ServiceAuthInfo = await providerClaimsService
                    .FindOrDefault(request.CategoryId, request.ProviderId);

            logger.LogInformation("Build auth info result.");

            return new AuthInfoDto
            {
                IsValid = true,
                Details = new AuthDetailsDto
                {
                    AuthToken = accessTokenService.Create(accessTokenData),
                    SessionToken = refreshToken
                }
            };
        }

        private static JwtAccessTokenDto CreateAccessTokenData(string userId, string sessionId) => new()
        {
            UserId = userId,
            SessionId = sessionId
        };
    }
}
