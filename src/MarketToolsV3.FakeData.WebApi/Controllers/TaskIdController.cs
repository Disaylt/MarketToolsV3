using MarketToolsV3.FakeData.WebApi.Application.Notifications;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketToolsV3.FakeData.WebApi.Controllers
{
    [Route("api/tasks/{id}")]
    [ApiController]
    public class TaskIdController(IPublisher<RunFakeDataTaskNotification> publisher, ITaskService taskService)
        : ControllerBase
    {
        [HttpPut]
        [Route("run")]
        public async Task<IActionResult> Run(string id)
        {
            RunFakeDataTaskNotification notification = new()
            {
                TaskId = id
            };

            await publisher.Notify(notification);

            return Ok();
        }

        [HttpPut]
        [Route("state")]
        public async Task<IActionResult> SetStateAsync([FromBody] NewTaskState body, string id)
        {
            await taskService.SetState(id, body.State);

            return Ok();
        }
    }
}
