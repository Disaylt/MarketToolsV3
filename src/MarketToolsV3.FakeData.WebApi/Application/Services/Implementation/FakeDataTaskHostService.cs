using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class FakeDataTaskHostService(IServiceProvider serviceProvider)
        : IFakeDataTaskHostService
    {
        public async Task RunTask(FakeDataTaskNotification notification)
        {
            using var scope = serviceProvider.CreateScope();


        }
    }
}
