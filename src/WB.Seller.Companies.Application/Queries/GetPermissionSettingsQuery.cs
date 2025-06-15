using MediatR;
using WB.Seller.Companies.Application.Models;

namespace WB.Seller.Companies.Application.Queries;

public class GetPermissionSettingsQuery : IRequest<IEnumerable<PermissionSettingDto>>
{
    public required string SubscriberId { get; set; }
    public required string OwnerId { get; set; }
    public int CompanyId { get; set; }
}