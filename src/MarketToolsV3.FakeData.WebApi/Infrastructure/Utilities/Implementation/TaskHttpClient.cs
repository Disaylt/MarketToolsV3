using MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Implementation
{
    public class TaskHttpClient(HttpMessageHandler handler)
        : HttpClient(handler), ITaskHttpClient
    {

    }
}
