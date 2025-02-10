using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class NotificationService : INotificationService
    {
        public event Func<FakeDataTaskNotification, Task>? RunTask;
        public event Func<TimerDataNotification, Task>? DelayNextTask;
    }
}
