using Grpc.Core;
using Identity.Application.Queries;
using MediatR;
using Proto.Contract.Identity;

namespace Identity.GrpcService.Services;

public class SessionService(IMediator mediator)
    : Session.SessionBase
{
    public override async Task<SessionActiveStatusReply> GetActiveStatus(SessionInfoRequest request, ServerCallContext context)
    {
        CheckSessionActiveStatusQuery command = new CheckSessionActiveStatusQuery
        {
            RefreshToken = request.Token
        };

        bool isActive = await mediator.Send(command);

        return new SessionActiveStatusReply
        {
            IsActive = isActive
        };
    }
}