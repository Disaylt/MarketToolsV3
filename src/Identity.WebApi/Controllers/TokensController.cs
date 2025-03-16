using Asp.Versioning;
using Identity.Application.Commands;
using Identity.WebApi.Services.Implementation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/tokens")]
    [ApiController]
    [ApiVersion("1")]
    public class TokensController(IMediator mediator)
        : ControllerBase
    {
        [HttpPost("refresh")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] CreateAuthInfo body, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(body, cancellationToken);

            return Ok(result);
        }
    }
}
