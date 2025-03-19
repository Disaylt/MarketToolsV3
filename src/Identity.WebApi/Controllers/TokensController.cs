using Asp.Versioning;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Services.Abstract;
using Identity.WebApi.Models;
using Identity.WebApi.Services.Implementation;
using Identity.WebApi.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/tokens")]
    [ApiController]
    [ApiVersion("1")]
    public class TokensController(
        IMediator mediator,
        ICredentialsService credentialsService)
        : ControllerBase
    {
        [HttpPost("refresh")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> RefreshTokenAsync([FromBody] NewAuthInfo body, CancellationToken cancellationToken)
        {
            var command = new CreateAuthInfo
            {
                RefreshToken = body.RefreshToken,
                ModulePath = body.ModulePath, 
                ModuleId= body.ModuleId,
                ModuleType = body.ModuleType,
                UserAgent = HttpContext.Request.Headers.UserAgent.FirstOrDefault() ?? "Unknown"
            };

            var result = await mediator.Send(command, cancellationToken);

            if (result is { IsValid: true, Details: not null })
            {
                credentialsService.Refresh(result.Details.AuthToken, result.Details.SessionToken);
            }

            return Ok(result);
        }
    }
}
