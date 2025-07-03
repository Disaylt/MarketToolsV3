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
    IRepository<SubscriptionEntity> subscriptionRepository,
    IUnitOfWork unitOfWork)
    : IRequestHandler<AddNewSubscriberCommand, Unit>
{
    public async Task<Unit> Handle(AddNewSubscriberCommand request, CancellationToken cancellationToken)
    {
        var user = await userEntityService.FindUserByLoginAsync(request.Login)
                   ?? throw new RootServiceException(HttpStatusCode.NotFound, "Пользователь с таким логином не найден.");

        SubscriptionEntity newSubscription = new(user.SubId, request.CompanyId, request.Note, SubscriptionRole.AwaitConfirmation);
        SubscriptionCodeEntity subscriptionCode = CreateSubscriptionCode();
        newSubscription.AddCode(subscriptionCode);

        await subscriptionRepository.AddAsync(newSubscription, cancellationToken);
        await unitOfWork.SaveEntitiesAsync(cancellationToken);

        return Unit.Value;
    }

    private SubscriptionCodeEntity CreateSubscriptionCode()
    {
        string code = codeUtility.Generate();

        return new(code);
    }
}