using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Services.Implementation;

namespace MarketToolsV3.FakeData.WebApi.Application
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<INotificationHandler<TimeoutNotification>, TimeoutTaskHandler>();
            serviceCollection.AddScoped<INotificationHandler<FakeDataTaskNotification>, FakeDataTaskHandler>();
            serviceCollection.AddScoped<IFakeDataTaskService, FakeDataTaskService>();
            serviceCollection.AddScoped<IFakeDataTaskMapService, FakeDataTaskMapService>();


            serviceCollection.AddSingleton<ISubscriber<FakeDataTaskNotification>, FakeDataTaskSubscriber>();
            serviceCollection.AddSingleton<ISubscriber<TimeoutNotification>, TimeoutTaskSubscriber>();
            serviceCollection.AddSingleton(typeof(IPublisher<>), typeof(Publisher<>));

            return serviceCollection;
        }
    }
}
