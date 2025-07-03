using MarketToolV3.Authentication.Services.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WB.Seller.Companies.Application.Commands;
using WB.Seller.Companies.Application.Queries;
using WB.Seller.Companies.WebApi.Models.Companies.Subscriptions;

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

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NewSubscriptionModel body)
        {
            AddNewSubscriberCommand command = new()
            {
                CompanyId = body.CompanyId,
                Login = body.Login,
                Note = body.Note
            };

            await mediator.Send(command);

            return Ok();
        }
    }
}
