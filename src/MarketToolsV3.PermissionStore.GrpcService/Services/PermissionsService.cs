using Google.Protobuf;
using Google.Protobuf.Collections;
using Grpc.Core;
using MarketToolsV3.PermissionStore.Application.Commands;
using MarketToolsV3.PermissionStore.Application.Enums;
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
            Permissions = request.Permissions,
            ParentModules = request.ParentModules
        };

        await mediator.Send(command);

        return new EmptyReply();
    }

    public override async Task<PermissionsListReply> GetByFilter(FilterRequest request, ServerCallContext context)
    {
        var query = new GetPermissionsByFilterQuery
        {
            Module = request.Module
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

    public override async Task<PermissionTreeResponse> GetPermissionTree(PermissionTreeRequest request, ServerCallContext context)
    {
        var query = new GetPermissionTreeQuery
        {
            Module = request.Module,
            Permissions = request.Permissions
                .Select(x => new PermissionSettingDto
                {
                    Path = x.Path,
                    Status = (PermissionStatusEnum)x.Status
                })
        };

        var permissionsTree = await mediator.Send(query);

        var result = new PermissionTreeResponse();
        foreach (var permissionTree in permissionsTree)
        {
            var newPermissionTree = CreateTree(permissionTree);
            result.Permissions.Add(newPermissionTree);
        }

        return result;
    }

    private static PermissionSettingNode CreateTree(PermissionSettingNodeDto node)
    {
        PermissionSettingNode newNode = new()
        {
            Name = node.Name,
            Status = (PermissionStatus)node.Status,
            View = node.View
        };

        foreach (var nextNode in node.Nodes)
        {
            var nextNewNode = CreateTree(nextNode);
            newNode.Children.Add(nextNewNode);
        }

        return newNode;
    }
}