using Identity.Application.Models;
using Identity.Application.Services;
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

            Session session = new Session(user.Id, request.UserAgent);

            JwtRefreshTokenDto refreshTokenData = new JwtRefreshTokenDto { Id = session.Id };
            string refreshToken = refreshTokenService.Create(refreshTokenData);

            session.Token = refreshToken;

            await sessionService.AddAsync(session, cancellationToken);

            logger.LogInformation("Add new session - {id}", session.Id);

            JwtAccessTokenDto accessTokenData = CreateAccessTokenData(user.Id, session.Id);

            return new AuthResultDto
            {
                AuthDetails = new AuthDetailsDto
                {
                    AuthToken = accessTokenService.Create(accessTokenData),
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

        private JwtAccessTokenDto CreateAccessTokenData(string userId, string sessionId)
        {
            return new JwtAccessTokenDto
            {
                UserId = userId,
                SessionId = sessionId,
            };
        }
    }
}
