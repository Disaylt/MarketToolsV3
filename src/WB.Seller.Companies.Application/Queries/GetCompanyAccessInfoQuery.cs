using MediatR;
using WB.Seller.Companies.Application.Models;

namespace WB.Seller.Companies.Application.Queries;

public class GetCompanyAccessInfoQuery : IRequest<AccessInfoDto>
{
    public required string UserId { get; set; }
    public required int CompanyId { get; set; }
}