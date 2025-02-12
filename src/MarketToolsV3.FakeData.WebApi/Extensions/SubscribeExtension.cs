using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Extensions
{
    public static class SubscribeExtension
    {
        public static WebApplication Subscribe<T>(this WebApplication webApplication) 
            where T : BaseNotification
        {
            ISubscriber<T> subscriber = webApplication
                .Services
                .GetRequiredService<ISubscriber<T>>();

            webApplication
                .Services
                .GetRequiredService<IPublisher<T>>()
                .Subscribe(subscriber);

            return webApplication;
        }
    }
}
