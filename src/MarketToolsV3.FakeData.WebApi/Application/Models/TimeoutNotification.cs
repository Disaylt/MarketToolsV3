namespace MarketToolsV3.FakeData.WebApi.Application.Models
{
    public class TimeoutNotification : Notification
    {
        public int TaskId { get; set; }
        public int Milliseconds { get; set; }
    }
}
