using System.Net;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.Cookies.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Models;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Abstract;
using Microsoft.Extensions.Options;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Implementation
{
    public class TaskHttpClientFactory(ITaskHttpLockStore taskHttpLockStore,
        ICookieContainerBackgroundService cookieContainerBackgroundService,
        IOptions<ServiceConfig> serviceConfigOptions,
        ILoggerFactory loggerFactory)
        : ITaskHttpClientFactory
    {
        private readonly Dictionary<string, HttpClientHandlerInfoModel> _idAndInfoPair = new();
        public async Task<ITaskHttpClient> CreateAsync(string id)
        {
            SemaphoreSlim semaphoreSlim = taskHttpLockStore.GetOrCreate(id);
            try
            {
                await semaphoreSlim.WaitAsync();


                if (!_idAndInfoPair.TryGetValue(id, out var info) ||
                    DateTime.UtcNow - _idAndInfoPair[id].Created > TimeSpan.FromMinutes(5))
                {
                    info?.Handler.Dispose();
                    await RefreshHttpInfoAsync(id);
                }

                var taskHttpClient = new TaskHttpClient(
                    _idAndInfoPair[id],
                    cookieContainerBackgroundService,
                    loggerFactory.CreateLogger<TaskHttpClient>());

                taskHttpClient.BaseAddress = new Uri(serviceConfigOptions.Value.BaseAddress
                                                     ?? throw new NullReferenceException());

                return new ConcurrentTaskHttpClient(taskHttpClient, _idAndInfoPair[id], taskHttpLockStore);
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
