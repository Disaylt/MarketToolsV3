using Grpc.Core;
using Identity.Application.Models;
using Identity.Application.Queries;
using MediatR;
using Proto.Contract.Identity;

namespace Identity.GrpcService.Services;

public class SessionService(IMediator mediator)
    : Session.SessionBase
{
    public override async Task<SessionActiveStatusReply> GetActiveStatus(SessionInfoRequest request, ServerCallContext context)
    {
        GetSessionDetailsQuery query = new()
        {
            Id = request.Id
        };

        SessionStatusDto sessionStatus = await mediator.Send(query);

        return new SessionActiveStatusReply
        {
            IsActive = sessionStatus.IsActive
        };
    }
}