using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Extensions
{
    public static class SubscribeExtension
    {
        public static WebApplication Subscribe<T>(this WebApplication webApplication) 
            where T : Notification
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
