using MarketToolsV3.PermissionStore.Application.Models;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Commands;

public class RefreshPermissionsCommand : IRequest<Unit>
{
    public required string Module { get; set; }
    public IReadOnlyCollection<ModulePermissionDto> Permissions { get; set; } = [];
}