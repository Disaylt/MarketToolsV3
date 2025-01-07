using Identity.Application.Models;
using MediatR;

namespace Identity.Application.Queries;

public class GetIdentityDetailsQuery : IRequest<IdentityDetailsDto>
{
    public required string UserId {get; set; }
}