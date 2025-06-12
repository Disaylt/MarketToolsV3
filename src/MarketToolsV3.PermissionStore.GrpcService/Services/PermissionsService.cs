using Google.Protobuf;
using Google.Protobuf.Collections;
using Grpc.Core;
using MarketToolsV3.PermissionStore.Application.Commands;
using MarketToolsV3.PermissionStore.Application.Models;
using MarketToolsV3.PermissionStore.Application.Queries;
using MediatR;
using Proto.Contract.Common.PermissionStore;

namespace MarketToolsV3.PermissionStore.GrpcService.Services;

public class PermissionsService(IMediator mediator) : Permission.PermissionBase
{

    public override async Task<EmptyReply> Refresh(RefreshRequest request, ServerCallContext context)
    {
        var command = new RefreshPermissionsCommand
        {
            Module = request.Module,
            Permissions = [.. request
                .Permissions]
        };

        await mediator.Send(command);

        return new EmptyReply();
    }

    public override async Task<PermissionsListReply> GetByFilter(FilterRequest request, ServerCallContext context)
    {
        var query = new GetPermissionsByFilterQuery
        {

        };

        var response = await mediator.Send(query);

        var permissions = response
            .Select(p => new ModulePermission
            {
                Path = p.Path,
                ViewName = p.ViewName
            });

        var result = new PermissionsListReply();
        result.Permissions.AddRange(permissions);

        return result;
    }

    public override Task<PermissionTreeResponse> GetPermissionTree(PermissionTreeRequest request, ServerCallContext context)
    {
        return base.GetPermissionTree(request, context);
    }
}