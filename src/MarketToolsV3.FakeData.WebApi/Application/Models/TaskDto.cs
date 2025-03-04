using MarketToolsV3.FakeData.WebApi.Domain.Enums;

namespace MarketToolsV3.FakeData.WebApi.Application.Models
{
    public class TaskDto
    {
        public required string Id { get; set; }
        public TaskState State { get; set; }
    }
}
