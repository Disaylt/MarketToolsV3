using Identity.Application.Models;
using Identity.Application.Services;
using Identity.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application.Commands
{
    public class CreateNewUserCommandHandler(IIdentityPersonService identityPersonService,
        ISessionService sessionService,
        ITokenService<JwtAccessTokenDto> accessTokenService,
        ITokenService<JwtRefreshTokenDto> refreshTokenService)
        : IRequestHandler<CreateNewUserCommand, AuthDetailsDto>
    {
        public async Task<AuthDetailsDto> Handle(CreateNewUserCommand request, CancellationToken cancellationToken)
        {
            IdentityPerson user = CreateUser(request.Email, request.Login);
            await identityPersonService.AddAsync(user, request.Password);

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
