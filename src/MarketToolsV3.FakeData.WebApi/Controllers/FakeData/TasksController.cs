using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;

namespace MarketToolsV3.FakeData.WebApi.Controllers.FakeData
{
    [Route("api/fake-data/tasks")]
    [ApiController]
    public class TasksController(IFakeDataTaskService fakeDataTaskService,
        IJsonValueService jsonValueService) 
        : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] IReadOnlyCollection<NewFakeDataTaskDto> body)
        {
            FakeDataTaskDto result = await fakeDataTaskService.CreateAsync(body);

            return Ok(result);
        }

        [HttpPost]
        [Route("test")]
        public IActionResult Test(JsonNode value)
        {
            var result = jsonValueService.FindRandomTemplateValues(value);
            jsonValueService.GenerateRandomValues(result);

            return Ok(value.ToJsonString());
        }
    }
}
