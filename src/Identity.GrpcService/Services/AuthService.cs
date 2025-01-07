using Grpc.Core;
using Identity.Application.Commands;
using Identity.Application.Models;
using MediatR;
using Proto.Contract.Identity;

namespace Identity.GrpcService.Services
{
    public class AuthService(IMediator mediator)
        : Auth.AuthBase
    {
        public override async Task<AuthInfoReply> GetAuthInfo(AuthInfoRequest request, ServerCallContext context)
        {
            CreateAuthInfo command = CreateAuthInfoCommand(request);

            AuthInfoDto authInfo = await mediator.Send(command);

            return CreateAuthInfoResponse(authInfo);
        }

        private AuthInfoReply CreateAuthInfoResponse(AuthInfoDto authInfo)
        {
            return new AuthInfoReply
            {
                IsValid = authInfo.IsValid,
                Refreshed = authInfo.Refreshed
            };
        }

        private CreateAuthInfo CreateAuthInfoCommand(AuthInfoRequest request)
        {
            return new CreateAuthInfo
            {
                UserAgent = request.UserAgent,
                Details = new AuthDetailsDto
                {
                    AuthToken = request.Details.AuthToken,
                    SessionToken = request.Details.SessionToken
                }
            };
        }
    }
}
