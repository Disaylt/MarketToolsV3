using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketToolsV3.FakeData.WebApi.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TasksController(ITaskService taskService)
        : ControllerBase
    {
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await taskService.GetAllAsync();

            return Ok(result);
        }
    }
}
