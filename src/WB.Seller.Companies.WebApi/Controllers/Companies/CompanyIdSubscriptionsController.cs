using MarketToolV3.Authentication.Services.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WB.Seller.Companies.Application.Queries;

namespace WB.Seller.Companies.WebApi.Controllers.Companies
{
    [Route("api/v{version:apiVersion}/companies/{companyId:int}/subscriptions")]
    [ApiController]
    [ApiVersion("1")]
    [Authorize]
    public class CompanyIdSubscriptionsController(
        IMediator mediator, 
        IAuthContext authContext)
        : ControllerBase
    {
        [HttpGet("details")]
        public async Task<IActionResult> GetAggregateAsync(int companyId)
        {
            GetCompanyAccessInfoQuery query = new()
            {
                CompanyId = companyId,
                UserId = authContext.GetUserIdRequired()
            };

            var result = await mediator.Send(query);

            return Ok(result);
        }
    }
}
