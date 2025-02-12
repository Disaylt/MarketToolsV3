using MarketToolsV3.FakeData.WebApi.Application.Notifications;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface ISubscriber<in T> where T : BaseNotification
    {
        Task HandleAsync(T notification);
    }
}
