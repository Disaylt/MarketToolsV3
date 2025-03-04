using System.Net;
using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Controllers.Details
{
    [Route("api/tasks/details")]
    [ApiController]
    public class DetailsController(ITaskService fakeDataTaskService)
        : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] IReadOnlyCollection<NewTaskDetailsDto> body)
        {
            TaskDto result = await fakeDataTaskService.CreateAsync(body);

            return Ok(result);
        }
    }
}
