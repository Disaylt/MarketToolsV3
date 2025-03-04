namespace MarketToolsV3.FakeData.WebApi.Application.Notifications
{
    public class RunFakeDataTaskNotification : BaseNotification
    {
        public required string TaskId { get; set; }
    }
}
