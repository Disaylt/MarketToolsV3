using Identity.Application.Services.Abstract;
using Identity.Application.Utilities.Abstract;
using Identity.Domain.Entities;
using Identity.Domain.Seed;
using MediatR;

namespace Identity.Application.Commands;

public class GenerateNewConfirmationCodeHandler(ICodeUtility codeUtility,
    IIdentityPersonService personService)
    : IRequestHandler<GenerateNewConfirmationCode, Unit>
{
    public async Task<Unit> Handle(GenerateNewConfirmationCode request, CancellationToken cancellationToken)
    {
        string code = codeUtility.Generate(6);
        await personService.SetNewConfirmationCodeAsync(request.Email, code, cancellationToken);

        return Unit.Value;
    }
}