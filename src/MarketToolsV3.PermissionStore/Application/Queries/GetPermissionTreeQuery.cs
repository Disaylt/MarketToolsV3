using MarketToolsV3.PermissionStore.Application.Models;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionTreeQuery : IRequest<IReadOnlyCollection<PermissionSettingNodeDto>>
{
    public IReadOnlyCollection<PermissionSettingDto> Permissions { get; set; } = [];
    public string SkipPath { get; set; } = string.Empty;
}