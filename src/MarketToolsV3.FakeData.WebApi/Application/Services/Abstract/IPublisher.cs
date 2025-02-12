using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Notifications;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface IPublisher<T> where T : BaseNotification
    {
        void Subscribe(ISubscriber<T> subscriber);
        void Unsubscribe(ISubscriber<T> subscriber);
        Task Notify(T notification);
    }
}
