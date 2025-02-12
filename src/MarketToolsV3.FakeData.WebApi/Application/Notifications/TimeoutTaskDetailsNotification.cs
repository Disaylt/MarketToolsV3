namespace MarketToolsV3.FakeData.WebApi.Application.Notifications
{
    public class TimeoutTaskDetailsNotification : BaseNotification
    {
        public int TaskId { get; set; }
        public int Milliseconds { get; set; }
    }
}
