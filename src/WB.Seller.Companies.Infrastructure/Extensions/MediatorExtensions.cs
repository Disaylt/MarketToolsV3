using MediatR;

namespace WB.Seller.Companies.Infrastructure.Extensions;

public static class MediatorExtensions
{
    public static async Task PublishNotifications(this IMediator mediator, IEnumerable<INotification> notifications, CancellationToken cancellationToken)
    {
        foreach (var notification in notifications)
        {
            await mediator.Publish(notification, cancellationToken);
        }
    }
}