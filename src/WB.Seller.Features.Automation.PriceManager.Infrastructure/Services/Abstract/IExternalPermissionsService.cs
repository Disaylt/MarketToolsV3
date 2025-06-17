namespace WB.Seller.Features.Automation.PriceManager.Infrastructure.Services.Abstract
{
    public interface IExternalPermissionsService
    {
        Task RefreshPermissionsAsync();
    }
}
