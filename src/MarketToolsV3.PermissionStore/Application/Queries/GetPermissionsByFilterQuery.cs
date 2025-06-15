using MarketToolsV3.PermissionStore.Application.Models;
using MediatR;

namespace MarketToolsV3.PermissionStore.Application.Queries;

public class GetPermissionsByFilterQuery : IRequest<IEnumerable<PermissionViewDto>>
{
    public required string Module { get; set; }
}