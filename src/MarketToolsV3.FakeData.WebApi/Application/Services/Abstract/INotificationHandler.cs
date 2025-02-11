using MarketToolsV3.FakeData.WebApi.Application.Models;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface INotificationHandler<in T>
        where T : Notification
    {
        Task HandleAsync(T notification);
    }
}
