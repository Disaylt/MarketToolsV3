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
            AuthInfoReply value = new AuthInfoReply
            {
                IsValid = authInfo.IsValid
            };

            if (authInfo.Details != null)
            {
                value.HasDetails = true;
                value.Details = new AuthInfoReply.Types.AuthDetails
                {
                    AuthToken = authInfo.Details.AuthToken,
                    SessionToken = authInfo.Details.SessionToken
                };
            }

            return value;
        }

        private CreateAuthInfo CreateAuthInfoCommand(AuthInfoRequest request)
        {
            return new CreateAuthInfo
            {
                UserAgent = request.UserAgent,
                RefreshToken = request.SessionToken
            };
        }
    }
}
