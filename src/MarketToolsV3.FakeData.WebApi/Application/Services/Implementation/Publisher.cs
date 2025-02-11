using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class Publisher<T> : IPublisher<T>
    {
        private readonly ICollection<ISubscriber<T>> _subscribers = [];
        public Task Notify(T notification)
        {
            foreach (var subscriber in _subscribers)
            {
                Task.Run(() => subscriber.HandleAsync(notification));
            }

            return Task.CompletedTask;
        }

        public void Subscribe(ISubscriber<T> subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(ISubscriber<T> subscriber)
        {
            _subscribers.Remove(subscriber);
        }
    }
}
