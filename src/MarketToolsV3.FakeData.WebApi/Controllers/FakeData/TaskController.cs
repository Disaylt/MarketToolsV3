using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarketToolsV3.FakeData.WebApi.Controllers.FakeData
{
    [Route("api/fake-data/tasks/{id:int}")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [HttpPut]
        [Route("run")]
        public IActionResult Run(int id)
        {
            return Ok();
        }
    }
}
