using MarketToolsV3.PermissionStore.Application.Models;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Commands;

public class RefreshPermissionsCommand : IRequest<Unit>
{
    public IReadOnlyCollection<string> Permissions { get; set; } = [];
    public required string Module { get; set; }
}