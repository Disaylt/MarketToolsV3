using System.Net;
using System.Security.Claims;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.WebApi.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetUserIdRequired(this HttpContext httpContext)
        {
            return httpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)
                   ?? throw new RootServiceException(HttpStatusCode.Unauthorized, "Не удалось получить идентификатор пользоватаеля.");
        }
    }
}
