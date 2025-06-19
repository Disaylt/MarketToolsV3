using MediatR;
using WB.Seller.Companies.Application.Models;

namespace WB.Seller.Companies.Application.Queries;

public class GetPermissionSettingsTreeQueryHandler
: IRequestHandler<GetPermissionSettingsTreeQuery, PermissionSettingNodeDto>
{
    public Task<PermissionSettingNodeDto> Handle(GetPermissionSettingsTreeQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}