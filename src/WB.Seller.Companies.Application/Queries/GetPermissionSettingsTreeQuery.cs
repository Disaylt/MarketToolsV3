using MediatR;
using WB.Seller.Companies.Application.Models;

namespace WB.Seller.Companies.Application.Queries;

public class GetPermissionSettingsTreeQuery : IRequest<PermissionSettingNodeDto>
{
    public required string UserId { get; set; }
}