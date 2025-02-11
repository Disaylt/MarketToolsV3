using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketToolsV3.FakeData.WebApi.Controllers.FakeData
{
    [Route("api/fake-data/tasks/{id:int}")]
    [ApiController]
    public class TaskController(IPublisher<FakeDataTaskNotification> publisher)
        : ControllerBase
    {
        [HttpPut]
        [Route("run")]
        public async Task<IActionResult> Run(int id)
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
