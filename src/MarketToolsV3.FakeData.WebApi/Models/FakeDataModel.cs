

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Nodes;

namespace MarketToolsV3.FakeData.WebApi.Models
{
    public class FakeDataModel
    {
        public string? Path { get; set; }

        [Required(ErrorMessage = "Необходимо ввести метод HTTP запроса.")]
        public required string Method { get; set; }
        public JsonNode? Body { get; set; }
    }
}
