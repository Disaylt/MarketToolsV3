namespace WB.Seller.Features.Automation.PriceManager.Application.Services.Abstract
{
    public interface IExternalPermissionsService
    {
        Task RefreshPermissionsAsync();
    }
}
