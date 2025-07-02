using MarketToolsV3.PermissionStore.Application.Models;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Commands;

public class RefreshPermissionsCommand : IRequest<Unit>
{
    public required string Creator { get; set; }
    public IEnumerable<PermissionDto> Permissions { get; set; } = [];
}