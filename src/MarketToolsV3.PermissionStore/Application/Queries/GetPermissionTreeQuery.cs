using MarketToolsV3.PermissionStore.Application.Models;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionTreeQuery : IRequest<IReadOnlyCollection<PermissionSettingNodeDto>>
{
    public IEnumerable<PermissionSettingDto> Permissions { get; set; } = [];
    public required string Module { get; set; }
}