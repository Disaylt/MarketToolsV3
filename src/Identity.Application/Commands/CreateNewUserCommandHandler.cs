using Identity.Application.Models;
using Identity.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Identity.Application.Services.Abstract;

namespace Identity.Application.Commands
{
    public class CreateNewUserCommandHandler(IIdentityPersonService identityPersonService,
        ILogger<CreateNewUserCommandHandler> logger,
        ISessionService sessionService,
        ITokenService<JwtAccessTokenDto> accessTokenService,
        ITokenService<JwtRefreshTokenDto> refreshTokenService)
        : IRequestHandler<CreateNewUserCommand, AuthResultDto>
    {
        public async Task<AuthResultDto> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
        {
            IdentityPerson user = CreateUser(request.Email, request.Login);
            await identityPersonService.AddAsync(user, request.Password);

            logger.LogInformation("Add new user - {id}", user.Id);

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
                    SessionToken = refreshToken
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
                SessionId = sessionId
            };
        }

        private IdentityPerson CreateUser(string email, string login)
        {
            return new IdentityPerson
            {
                Email = email,
                UserName = login
            };
        }
    }
}
