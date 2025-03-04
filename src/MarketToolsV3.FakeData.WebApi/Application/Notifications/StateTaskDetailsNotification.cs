namespace MarketToolsV3.FakeData.WebApi.Application.Notifications
{
    public class StateTaskDetailsNotification : BaseNotification
    {
        public int TaskDetailsId { get; set; }
        public bool Success { get; set; }
    }
}
