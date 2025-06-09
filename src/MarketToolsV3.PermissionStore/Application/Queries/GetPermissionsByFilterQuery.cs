using MarketToolsV3.PermissionStore.Application.Models;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionsByFilterQuery : IRequest<IEnumerable<PermissionViewDto>>
{
    public IEnumerable<string> Modules { get; set; } = [];
}