using MarketToolsV3.FakeData.WebApi.Application.Notifications;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface ISafeSubscriberHandler
    {
        Task SafeRunAsync<T>(ISubscriber<T> subscriber, T notification) where T : BaseNotification;
    }
}
