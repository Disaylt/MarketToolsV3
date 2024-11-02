using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands
{
    public class CreateAuthInfoHandler(IRepository<Session> sessionRepository,
        ITokenService<JwtAccessTokenDto> accessTokenService,
        ITokenService<JwtRefreshTokenDto> refreshTokenService,
        ISessionService sessionService)
        : IRequestHandler<CreateAuthInfo, AuthInfoDto>
    {
        public async Task<AuthInfoDto> Handle(CreateAuthInfo request, CancellationToken cancellationToken)
        {
            if (await accessTokenService.IsValid(request.Details.AuthToken))
            {
                return new AuthInfoDto { IsValid = true };
            }

            if (await refreshTokenService.IsValid(request.Details.SessionToken) == false)
            {
                return new AuthInfoDto { IsValid = false };
            }

            JwtRefreshTokenDto refreshTokenData = refreshTokenService.Read(request.Details.SessionToken);

            Session session = await sessionRepository.FindByIdRequiredAsync(refreshTokenData.Id, cancellationToken);

            if (session.IsActive == false || session.Token != request.Details.SessionToken)
            {
                return new AuthInfoDto { IsValid = false };
            }

            string refreshToken = refreshTokenService.Create(refreshTokenData);

            await sessionService.UpdateAsync(session, refreshToken, request.UserAgent, cancellationToken);

            JwtAccessTokenDto accessTokenData = CreateAccessTokenData(session.IdentityId);

            return new AuthInfoDto
            {
                IsValid = true,
                Refreshed = true,
                Details = new AuthDetailsDto
                {
                    AuthToken = accessTokenService.Create(accessTokenData),
                    SessionToken = refreshToken
                }
            };
        }

        private JwtAccessTokenDto CreateAccessTokenData(string userId)
        {
            return new JwtAccessTokenDto
            {
                UserId = userId
            };
        }
    }
}
