using System.Net;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Domain.Entities
{
    public class ValueUseHistory : Entity
    {
        public required string Path { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public ResponseBody ResponseBody { get; set; } = null!;
        public int ResponseBodyId { get; set; }
    }
}
