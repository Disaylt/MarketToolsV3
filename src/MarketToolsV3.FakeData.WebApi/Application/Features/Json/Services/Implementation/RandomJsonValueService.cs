using MarketToolsV3.FakeData.WebApi.Application.Features.Json.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Models;
using MarketToolsV3.FakeData.WebApi.Application.Utilities.Abstract;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Application.Features.Json.Services.Implementation
{
    public class RandomJsonValueService(
        IRandomTemplateParser randomTemplateParser,
        IJsonValueRandomizeService<int> intJsonValueRandomizeService,
        IJsonValueRandomizeService<string> stringJsonValueRandomizeService)
        : IRandomJsonValueService
    {

        public void GenerateRandomValue(JsonValue value)
        {
            if (value.GetValueKind() != JsonValueKind.String)
            {
                return;
            }

            string data = value.GetValue<string>();
            JsonRandomTemplateModel typingData = randomTemplateParser.Parse(data);

            switch (typingData.Type)
            {
                case "num":
                    int num = intJsonValueRandomizeService.Create(typingData.Min, typingData.Max);
                    value.ReplaceWith(num);
                    break;
                case "str":
                    string str = stringJsonValueRandomizeService.Create(typingData.Min, typingData.Max);
                    value.ReplaceWith(str);
                    break;
            }
        }

        public void GenerateRandomValues(IEnumerable<JsonValue> values)
        {
            foreach (var value in values)
            {
                GenerateRandomValue(value);
            }
        }
    }
}
