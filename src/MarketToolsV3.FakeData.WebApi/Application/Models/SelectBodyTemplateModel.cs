namespace MarketToolsV3.FakeData.WebApi.Application.Models
{
    public class SelectBodyTemplateModel
    {
        public required string Tag { get; set; }

        public string[] Paths { get; set; } = [];
    }
}
