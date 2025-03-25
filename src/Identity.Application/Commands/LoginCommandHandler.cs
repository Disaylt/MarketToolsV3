using Identity.Application.Models;
using Identity.Application.Services.Abstract;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands
{
    public class LoginCommandHandler(IIdentityPersonService userService,
        ISessionService sessionService,
        ILogger<LoginCommandHandler> logger,
        ITokenService<JwtAccessTokenDto> accessTokenService,
        ITokenService<JwtRefreshTokenDto> refreshTokenService)
        : IRequestHandler<LoginCommand, AuthResultDto>
    {
        public async Task<AuthResultDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            IdentityPerson? user = await userService.FindByEmailAsync(request.Email);

            if (user == null || await userService.CheckPassword(user, request.Password) == false)
            {
                logger.LogWarning("User {id} failed password verification", user?.Id);

                throw new RootServiceException(HttpStatusCode.NotFound, "Неверно введены почта или пароль.");
            }

            Session session = new(user.Id, request.UserAgent);

            JwtAccessTokenDto accessTokenData = CreateAccessTokenData(user.Id, session.Id);
            string accessToken = accessTokenService.Create(accessTokenData);

            JwtRefreshTokenDto refreshTokenData = new()
            {
                Id = session.Id,
                AccessTokenId = accessTokenData.Id
            };
            string refreshToken = refreshTokenService.Create(refreshTokenData);

            session.Token = refreshToken;

            await sessionService.AddAsync(session, cancellationToken);

            logger.LogInformation("Add new session - {id}", session.Id);

            return new AuthResultDto
            {
                AuthDetails = new AuthDetailsDto
                {
                    AuthToken = accessToken,
                    SessionToken = refreshToken,
                },
                IdentityDetails = new IdentityDetailsDto
                {
                    Id = user.Id,
                    Email = user.Email ?? "Неизвестно",
                    Name = user.UserName ?? "Неизвестно"
                }
            };
        }

        private static JwtAccessTokenDto CreateAccessTokenData(string userId, string sessionId)
        {
            return new()
            {
                UserId = userId,
                SessionId = sessionId,
                Id = Guid.NewGuid().ToString()
            };
        }
    }
}
