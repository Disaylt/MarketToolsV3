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
using MassTransit.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Identity.Application.Commands
{
    public class CreateAuthInfoCommandHandler(IRepository<Session> sessionRepository,
        ILogger<CreateAuthInfoCommandHandler> logger,
        ITokenService<JwtAccessTokenDto> accessTokenService,
        ITokenService<JwtRefreshTokenDto> refreshTokenService,
        ISessionService sessionService,
        IModulePermissionsService modulePermissionsService,
        IAccessTokenBlacklistService accessTokenBlacklistService)
        : IRequestHandler<CreateAuthInfoCommand, AuthInfoDto>
    {
        public async Task<AuthInfoDto> Handle(CreateAuthInfoCommand request, CancellationToken cancellationToken)
        {
            if (await refreshTokenService.IsValid(request.RefreshToken) == false)
            {
                logger.LogWarning("Refresh token isn't valid");

                return new AuthInfoDto { IsValid = false };
            }

            JwtRefreshTokenDto oldRefreshTokenData = refreshTokenService.Read(request.RefreshToken);

            Session session = await sessionRepository.FindByIdRequiredAsync(oldRefreshTokenData.Id, cancellationToken);

            if (session.IsActive == false 
                || session.Token != request.RefreshToken)
            {
                logger.LogWarning("Session status not active ({status}) or current refresh token does not match session refresh token.", session.IsActive);

                return new AuthInfoDto { IsValid = false };
            }

            JwtAccessTokenDto newAccessTokenData = CreateAccessTokenData(session.IdentityId, session.Id);
            newAccessTokenData.ModuleAuthInfo = await modulePermissionsService
                    .FindOrDefault(request.ModulePath, session.IdentityId, request.ModuleId);

            JwtRefreshTokenDto newRefreshTokenData = CreateRefreshToken(session.Id, newAccessTokenData.Id);
            string refreshToken = refreshTokenService.Create(newRefreshTokenData);
            await sessionService.UpdateAsync(session, refreshToken, request.UserAgent, cancellationToken);

            var newAuthData = new AuthInfoDto
            {
                IsValid = true,
                Details = new AuthDetailsDto
                {
                    AuthToken = accessTokenService.Create(newAccessTokenData),
                    SessionToken = refreshToken
                }
            };

            if (string.IsNullOrEmpty(oldRefreshTokenData.AccessTokenId) == false)
            {
                await accessTokenBlacklistService.AddAsync(oldRefreshTokenData.AccessTokenId);
            }

            return newAuthData;
        }

        private static JwtRefreshTokenDto CreateRefreshToken(string sessionId, string accessTokenId)
        {
            return new()
            {
                Id = sessionId,
                AccessTokenId = accessTokenId
            };
        }

        private static JwtAccessTokenDto CreateAccessTokenData(string userId, string sessionId) => new()
        {
            UserId = userId,
            Id = Guid.NewGuid().ToString(),
            SessionId = sessionId
        };
    }
}
