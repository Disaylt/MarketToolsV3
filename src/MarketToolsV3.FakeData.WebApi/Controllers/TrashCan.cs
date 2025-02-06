using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Nodes;
using System.Text.Json;
using MarketToolsV3.FakeData.WebApi.Application.Models;

namespace MarketToolsV3.FakeData.WebApi.Controllers
{
    public class TrashCan
    {
        private readonly List<JsonValue> _values = [];

        [HttpPost]
        public void CreateTaskAsync([FromBody] IReadOnlyCollection<NewFakeDataTaskDto> tasks)
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
                    value.ReplaceWith(Random.Shared.Next(1, 199));
                }
            }
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
