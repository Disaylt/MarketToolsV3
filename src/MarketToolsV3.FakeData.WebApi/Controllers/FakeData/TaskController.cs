using System.Text.Json;
using Google.Protobuf.WellKnownTypes;
using MarketToolsV3.FakeData.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Controllers.FakeData
{
    [Route("api/fake-data/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly List<JsonValue> _values = [];

        [HttpPost]
        public async Task<IActionResult> CreateTaskAsync([FromBody] IReadOnlyCollection<NewFakeDataModel> tasks)
        {
            ReplaceValues(tasks.First().Body);


            foreach (var value in _values)
            {
                if (value.GetValueKind() != JsonValueKind.String)
                {
                    continue;
                }

                if (value.GetValue<string>() == "random:str")
                {
                    value.ReplaceWith(Guid.NewGuid().ToString());
                }
                else if (value.GetValue<string>() == "random:num")
                {
                    value.ReplaceWith(Random.Shared.Next(1,199));
                }
            }

            return Ok(tasks.FirstOrDefault()?.Body);
        }

        private void ReplaceValues(JsonNode? node)
        {
            if (node is JsonObject jsonObject)
            {
                foreach (var property in jsonObject)
                {
                    ReplaceValues(property.Value);
                }
            }
            else if (node is JsonArray jsonArray)
            {
                foreach (var item in jsonArray)
                {
                    ReplaceValues(item);
                }
            }
            else if (node is JsonValue jsonValue)
            {
                if (jsonValue.TryGetValue(out string? strValue) && (strValue == "random:str" || strValue == "random:num"))
                {
                    _values.Add(jsonValue);
                }
            }
        }
    }
}
