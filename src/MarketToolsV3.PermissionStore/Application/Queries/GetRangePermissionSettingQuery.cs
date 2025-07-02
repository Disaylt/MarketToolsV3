using MarketToolsV3.PermissionStore.Application.Models;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetRangePermissionSettingQuery : IRequest<IEnumerable<PermissionSettingDto>>
{
    public required string Module { get; set; }
    public IEnumerable<PermissionSettingDto> Permissions { get; set; } = [];
}