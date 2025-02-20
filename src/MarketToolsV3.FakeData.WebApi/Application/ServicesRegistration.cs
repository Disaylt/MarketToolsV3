using MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers;
using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Services.Implementation;

namespace MarketToolsV3.FakeData.WebApi.Application
{
    public static class ServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<INotificationHandler<TimeoutTaskDetailsNotification>, TimeoutTaskDetailsHandler>();
            serviceCollection.AddScoped<INotificationHandler<RunFakeDataTaskNotification>, RunFakeDataTaskHandler>();
            serviceCollection.AddScoped<INotificationHandler<FailFakeDataTasksNotification>, FailFakeDataTasksHandler>();
            serviceCollection.AddScoped<INotificationHandler<ProcessTaskDetailsNotification>, ProcessTaskDetailsHandler>();
            serviceCollection.AddScoped<INotificationHandler<SelectTaskDetailsNotification>, SelectTaskDetailsHandler>();
            serviceCollection.AddScoped<INotificationHandler<StateTaskDetailsNotification>, StateTaskDetailsHandler>();
            serviceCollection.AddScoped<INotificationHandler<MarkAsHandleNotification>, MarkAsHandleHandler>();
            serviceCollection.AddScoped<INotificationHandler<MarkTaskAsAwaitNotification>, MarkTaskAsAwaitHandler>();
            serviceCollection.AddScoped<INotificationHandler<SkipGroupTasksNotification>, SkipGroupTasksHandler>();

            serviceCollection.AddScoped<IFakeDataTaskService, FakeDataTaskService>();
            serviceCollection.AddScoped<IFakeDataTaskMapService, FakeDataTaskMapService>();

            serviceCollection.AddSingleton(typeof(IPublisher<>), typeof(Publisher<>));
            serviceCollection.AddSingleton(typeof(ISubscriber<>), typeof(Subscriber<>));

            serviceCollection.AddSingleton<ISafeSubscriberHandler, SafeSubscriberHandler>();
            serviceCollection.AddSingleton<ITaskDetailsService, TaskDetailsService>();
            serviceCollection.AddSingleton<IRandomTemplateParser, RandomTemplateParser>();
            serviceCollection.AddSingleton<IJsonValueRandomizeService<string>, StrJsonValueRandomizeService>();
            serviceCollection.AddSingleton<IJsonValueRandomizeService<int>, IntJsonValueRandomizeService>();

            return serviceCollection;
        }
    }
}
