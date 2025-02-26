using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using System.Net;

namespace MarketToolsV3.FakeData.WebApi.Domain.Entities
{
    public class ResponseBody : Entity
    {
        public string? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public bool Used { get; set; } = false;
        public TaskDetails TaskDetails { get; set; } = null!;
        public int TaskDetailsId { get; set; }
    }
}
