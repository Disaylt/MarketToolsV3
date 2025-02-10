using MarketToolsV3.FakeData.WebApi.Application.Models;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface INotificationService
    {
        event Func<FakeDataTaskNotification,Task> RunTask;
        event Func<TimerDataNotification, Task> DelayNextTask;
    }
}
