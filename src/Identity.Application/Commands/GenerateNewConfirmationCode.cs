using Identity.Application.Seed;
using MediatR;

namespace Identity.Application.Commands;

public class GenerateNewConfirmationCode : ICommand<Unit>
{
    public required string UserId { get; set; }
}