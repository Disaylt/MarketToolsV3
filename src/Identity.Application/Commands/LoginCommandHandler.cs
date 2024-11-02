using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MediatR;
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
        ITokenService<JwtAccessTokenDto> accessTokenService,
        ITokenService<JwtRefreshTokenDto> refreshTokenService)
        : IRequestHandler<LoginCommand, AuthDetailsDto>
    {
        public async Task<AuthDetailsDto> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            IdentityPerson? user = await userService.FindByEmailAsync(request.Email);

            if (user == null || await userService.CheckPassword(user, request.Password) == false)
            {
                throw new RootServiceException(HttpStatusCode.NotFound, "Неверно введены почта или пароль.");
            }

            Session session = new Session(user.Id, request.UserAgent);

            JwtRefreshTokenDto refreshTokenData = new JwtRefreshTokenDto { Id = session.Id };
            string refreshToken = refreshTokenService.Create(refreshTokenData);

            session.Token = refreshToken;

            await sessionService.AddAsync(session, cancellationToken);

            JwtAccessTokenDto accessTokenData = CreateAccessTokenData(user.Id);

            return new AuthDetailsDto
            {
                AuthToken = accessTokenService.Create(accessTokenData),
                SessionToken = refreshToken
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
