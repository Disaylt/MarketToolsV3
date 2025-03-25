using System.ComponentModel;

namespace WB.Seller.Companies.Domain.Enums;

public enum SubscriptionRole
{
    [Description("Собственник")]
    Owner,

    [Description("Подписчик")]
    Subscriber
}