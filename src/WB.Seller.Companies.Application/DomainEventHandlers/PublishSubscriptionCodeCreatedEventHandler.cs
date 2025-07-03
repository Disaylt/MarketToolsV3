using IntegrationEvents.Contract.Profile;
using MarketToolsV3.ConfigurationManager.Models;
using MassTransit;
using MassTransit.Configuration;
using MediatR;
using Microsoft.Extensions.Options;
using WB.Seller.Companies.Domain.DomainEvents;
using WB.Seller.Companies.Domain.Entities;
using WB.Seller.Companies.Domain.Enums;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Application.DomainEventHandlers;

public class PublishSubscriptionCodeCreatedEventHandler(IPublishEndpoint publisherEndpoint,
    IRepository<SubscriptionEntity> subscriptionRepository,
    IRepository<CompanyEntity> companyRepository,
    IOptions<ServicesAddressesConfig> serviceAddresses)
    : INotificationHandler<CreateSubscriptionCodeDomainEvent>
{
    public async Task Handle(CreateSubscriptionCodeDomainEvent notification, CancellationToken cancellationToken)
    {
        var subscription = await subscriptionRepository.FindByIdRequiredAsync(notification.Entity.SubscriptionId, cancellationToken);
        var company = await companyRepository.FindByIdRequiredAsync(subscription.CompanyId, cancellationToken);

        CreateUserNotificationIntegrationEvent @event = new()
        {
            UserId = subscription.UserId,
            Category = serviceAddresses.Value.Wb.Seller.Companies.Name,
            Message = $"Ваш код для подписки на компанию '{company.Name}' - {notification.Entity.Code}. Действителен в течении 10 минут.",
            Title = "Код подписчика"
        };

        await publisherEndpoint.Publish(@event, cancellationToken);
    }
}