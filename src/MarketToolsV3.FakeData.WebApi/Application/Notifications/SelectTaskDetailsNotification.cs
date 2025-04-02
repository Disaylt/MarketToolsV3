namespace MarketToolsV3.FakeData.WebApi.Application.Notifications
{
    public class SelectTaskDetailsNotification : BaseNotification
    {
        public required string TaskId { get; set; }
    }
}
