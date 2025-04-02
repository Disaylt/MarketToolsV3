using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WB.Seller.Companies.Application.Queries;

namespace WB.Seller.Companies.WebApi.Controllers.ReferenceInformation
{
    [Route("api/v{version:apiVersion}/reference-information")]
    [ApiController]
    [ApiVersion("1")]
    public class ReferenceInformationController(IMediator mediator)
        : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion("1")]
        public async Task<IActionResult> GetAsync()
        {
            var result = await mediator.Send(new GetReferenceInformationQuery());

            return Ok(result);
        }
    }
}
