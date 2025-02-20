using Google.Protobuf.WellKnownTypes;
using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Services.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Implementation
{
    public class JsonValueService(
        IRandomTemplateParser randomTemplateParser)
        : IJsonValueService
    {
        public ICollection<JsonValue> FindRandomTemplateValues(JsonNode? node)
        {
            if (node is JsonObject jsonObject)
            {
                return ParseJsonObject(jsonObject);
            }
            if (node is JsonArray jsonArray)
            {
                return ParseJsonArray(jsonArray);
            }
            if (node is JsonValue jsonValue && 
                jsonValue.TryGetValue(out string? strValue) && 
                strValue.StartsWith("random"))
            {
                return [jsonValue];
            }

            return [];
        }

        public void GenerateRandomValue(JsonValue value)
        {
            if (value.GetValueKind() != JsonValueKind.String)
            {
                return;
            }

            string data = value.GetValue<string>();
            JsonRandomModel typingData = randomTemplateParser.Parse(data);

            switch (typingData.Type)
            {
                case "num":
                    int num = Random
                        .Shared
                        .Next(typingData.Min, typingData.Max);
                    value.ReplaceWith(num);
                    break;
                case "str":

                    break;
            }
        }

        public void GenerateRandomValues(IEnumerable<JsonValue> values)
        {
            throw new NotImplementedException();
        }

        private ICollection<JsonValue> ParseJsonArray(JsonArray jsonArray)
        {
            List<JsonValue> values = [];

            foreach (var item in jsonArray)
            {
                ICollection<JsonValue> arrayValues = FindRandomTemplateValues(item);
                values.AddRange(arrayValues);
            }

            return values;
        }

        private ICollection<JsonValue> ParseJsonObject(JsonObject jsonObject)
        {
            List<JsonValue> values = [];

            foreach (var property in jsonObject)
            {
                ICollection<JsonValue> objectValues = FindRandomTemplateValues(property.Value);
                values.AddRange(objectValues);
            }

            return values;
        }
    }
}
