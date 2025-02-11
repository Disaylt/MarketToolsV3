namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface ISubscriber<in T>
    {
        Task HandleAsync(T notification);
    }
}
