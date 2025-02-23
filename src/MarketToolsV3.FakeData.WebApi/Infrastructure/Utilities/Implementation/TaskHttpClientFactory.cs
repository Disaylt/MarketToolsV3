using System.Net;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Models;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Utilities.Implementation
{
    public class TaskHttpClientFactory(ITaskHttpLockStore taskHttpLockStore,
        ICookieContainerBackgroundService cookieContainerBackgroundService)
        : ITaskHttpClientFactory
    {
        private readonly Dictionary<string, HttpClientHandlerInfoModel> _idAndInfoPair = new();
        public async Task<ITaskHttpClient> CreateAsync(string id)
        {
            SemaphoreSlim semaphoreSlim = taskHttpLockStore.GetOrCreate(id);
            try
            {
                await semaphoreSlim.WaitAsync();

                if (_idAndInfoPair.ContainsKey(id) == false ||
                    DateTime.UtcNow - _idAndInfoPair[id].Created > TimeSpan.FromMinutes(5))
                {
                    _idAndInfoPair[id].Handler.Dispose();
                    await RefreshHttpInfoAsync(id);
                }

                return new TaskHttpClient(_idAndInfoPair[id], cookieContainerBackgroundService);
            }
            finally
            {
                semaphoreSlim.Release();
            }
        }

        private async Task RefreshHttpInfoAsync(string id)
        {
            HttpClientHandlerInfoModel info = new()
            {
                Handler = new()
                {
                    CookieContainer = await cookieContainerBackgroundService.CreateByTask(id)
                },
                Id = id
            };
            _idAndInfoPair.Add(id, info);
        }
    }
}
