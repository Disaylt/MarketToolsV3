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
            CreateAuthInfoCommand command = CreateAuthInfoCommand(request);

            AuthInfoDto authInfo = await mediator.Send(command);

            return CreateAuthInfoResponse(authInfo);
        }

        private static AuthInfoReply CreateAuthInfoResponse(AuthInfoDto authInfo)
        {
            AuthInfoReply value = new()
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

        private static CreateAuthInfoCommand CreateAuthInfoCommand(AuthInfoRequest request)
        {
            return new()
            {
                UserAgent = request.UserAgent,
                RefreshToken = request.SessionToken
            };
        }
    }
}
