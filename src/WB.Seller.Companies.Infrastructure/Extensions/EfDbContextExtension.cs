using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Infrastructure.Extensions;

public static class EfDbContextExtension
{
    public static IEnumerable<INotification> SelectNotifications(this ChangeTracker changeTracker)
    {
        List<INotification> notificationList = new List<INotification>();

        foreach (var entity in changeTracker.Entries<Entity>())
        {
            notificationList.AddRange(entity.Entity.DomainEvents);
        }

        return notificationList;
    }
}