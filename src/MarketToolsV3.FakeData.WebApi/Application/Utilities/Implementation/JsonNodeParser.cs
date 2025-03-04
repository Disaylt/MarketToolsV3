using MarketToolsV3.FakeData.WebApi.Application.Utilities.Abstract;
using System.IO;
using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Application.Utilities.Implementation
{
    public class JsonNodeParser : IJsonNodeParser
    {
        public JsonNode? ParseByPath(JsonNode node, params string[] pathName)
        {
            JsonNode? result = node;
            foreach (var part in pathName)
            {
                if (result == null)
                {
                    break;
                }

                if (part.EndsWith("]"))
                {
                    string[] arrayPart = part.Split('[', ']');
                    string arrayName = arrayPart[0];
                    int index = int.Parse(arrayPart[1]);
                    result = result[arrayName]?[index];
                }
                else
                {
                    result = result[part];
                }
            }
            return result;
        }
    }
}
