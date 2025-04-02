using System.Net;

namespace MarketToolsV3.FakeData.WebApi.Domain.Seed
{
    public class RootServiceException(HttpStatusCode statusCode = HttpStatusCode.BadRequest, params string[] messages) : Exception
    {
        public override string Message => string.Join('|', messages);
        public IEnumerable<string> Messages { get; } = messages;
        public HttpStatusCode StatusCode { get; } = statusCode;
    }
}
