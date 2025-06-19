using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.QueryData.Permissions;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController(IQueryDataHandler<SubscriptionAggregateQueryData, SubscriptionAggregateDto> queryDataHandler) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> TestAsync([FromBody] SubscriptionAggregateQueryData body)
        {
            var data = await queryDataHandler.HandleAsync(body);

            return Ok(data);
        }
    }
}
