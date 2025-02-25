using MarketToolsV3.FakeData.WebApi.Application.Mappers;
using MarketToolsV3.FakeData.WebApi.Application.NotificationHandlers;
using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Services.Implementation;
using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using System.Net;

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
            serviceCollection.AddScoped<ICookieContainerService, CookieContainerService>();

            serviceCollection.AddSingleton(typeof(IPublisher<>), typeof(Publisher<>));
            serviceCollection.AddSingleton(typeof(ISubscriber<>), typeof(Subscriber<>));

            serviceCollection.AddSingleton<IFromMapper<Cookie, CookieEntity>, CookieFromCookieEntityMapper>();
            serviceCollection.AddSingleton<IToMapper<CookieEntity, Cookie>, CookieToCookieEntityMapper>();
            serviceCollection.AddSingleton<ISafeSubscriberHandler, SafeSubscriberHandler>();
            serviceCollection.AddSingleton<ITaskDetailsService, TaskDetailsService>();
            serviceCollection.AddSingleton<IRandomTemplateParser, RandomTemplateParser>();
            serviceCollection.AddSingleton<IJsonValueService, JsonValueService>();
            serviceCollection.AddSingleton<IJsonValueRandomizeService<string>, StrJsonValueRandomizeService>();
            serviceCollection.AddSingleton<IJsonValueRandomizeService<int>, IntJsonValueRandomizeService>();

            return serviceCollection;
        }
    }
}
