using System.Net;
using MediatR;
using WB.Seller.Companies.Application.Services.Abstract;
using WB.Seller.Companies.Application.Utilities.Abstract;
using WB.Seller.Companies.Domain.Entities;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.Commands;

public class AddNewSubscriberCommandHandler(IUserEntityService userEntityService,
    ICodeUtility codeUtility,
    IRepository<SubscriptionEntity> subscriptionRepository)
    : IRequestHandler<AddNewSubscriberCommand, Unit>
{
    public async Task<Unit> Handle(AddNewSubscriberCommand request, CancellationToken cancellationToken)
    {
        var user = await userEntityService.FindUserByLoginAsync(request.Login)
                   ?? throw new RootServiceException(HttpStatusCode.NotFound, "Пользователь с таким логином не найден.");

        SubscriptionEntity newSubscription = new(user.SubId, request.CompanyId, request.Note, SubscriptionRole.AwaitConfirmation);
        string code = codeUtility.Generate();
        newSubscription.AddCode(code);

        await subscriptionRepository.AddAsync(newSubscription, cancellationToken);

        return Unit.Value;
    }
}