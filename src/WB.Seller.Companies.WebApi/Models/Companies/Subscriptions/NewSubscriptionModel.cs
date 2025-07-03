namespace WB.Seller.Companies.WebApi.Models.Companies.Subscriptions;

public class NewSubscriptionModel
{
    public required string Login { get; set; }
    public int CompanyId { get; set; }
    public string? Note { get; set; }
}