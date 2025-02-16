using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketToolsV3.FakeData.WebApi.Controllers.FakeData
{
    [Route("api/fake-data/tasks/{id}")]
    [ApiController]
    public class TaskController(IPublisher<FakeDataTaskNotification> publisher)
        : ControllerBase
    {
        [HttpPut]
        [Route("run")]
        public async Task<IActionResult> Run(string id)
        {
            FakeDataTaskNotification notification = new()
            {
                TaskId = id
            };

            await publisher.Notify(notification);

            return Ok();
        }
    }
}
