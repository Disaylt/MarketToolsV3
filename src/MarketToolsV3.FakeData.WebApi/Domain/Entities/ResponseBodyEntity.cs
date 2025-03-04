using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using System.Net;

namespace MarketToolsV3.FakeData.WebApi.Domain.Entities
{
    public class ResponseBodyEntity : Entity
    {
        public string? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public TaskDetailsEntity TaskDetail { get; set; } = null!;
        public int TaskDetailId { get; set; }
    }
}
