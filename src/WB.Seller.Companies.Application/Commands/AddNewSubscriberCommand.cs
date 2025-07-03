using MediatR;
using WB.Seller.Companies.Application.Seed;

namespace WB.Seller.Companies.Application.Commands;

public class AddNewSubscriberCommand : ICommand<Unit>
{
    public required string Login { get; set; }
    public int CompanyId { get; set; }
}