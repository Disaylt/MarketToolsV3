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
            Creator = request.Creator,
            Permissions = request.Permissions
                .Select(p => new PermissionDto
                {
                    Path = p.Path,
                    RequireUse = p.RequireUse,
                    AvailableModules = p.AvailableModules
                })
        };

        await mediator.Send(command);

        return new EmptyReply();
    }

    public override async Task<PermissionsListReply> GetRange(FilterRequest request, ServerCallContext context)
    {
        var query = new GetRangePermissionSettingQuery
        {
            Module = request.Module,
            Permissions = request.Permissions
                .Select(p => new PermissionSettingDto
                {
                    Path = p.Path,
                    Status = (PermissionStatusEnum)p.Status
                })
        };

        var response = await mediator.Send(query);

        return new PermissionsListReply
        {
            Permissions =
            {
                response.Select(x => new PermissionSetting
                {
                    Path = x.Path,
                    Status = (PermissionStatus)x.Status
                })
            }
        };
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
                    Status = (PermissionStatusEnum)x.Status,
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

    private static PermissionSettingNode CreateTree(PermissionSettingViewNodeDto node)
    {
        PermissionSettingNode newNode = new()
        {
            Name = node.Name,
            Status = (PermissionStatus)node.Status,
            Path = node.Path
        };

        foreach (var nextNode in node.Nodes)
        {
            var nextNewNode = CreateTree(nextNode);
            newNode.Children.Add(nextNewNode);
        }

        return newNode;
    }
}