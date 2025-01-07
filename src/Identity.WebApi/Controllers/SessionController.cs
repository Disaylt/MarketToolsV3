using Asp.Versioning;
using Identity.Application.Commands;
using Identity.Application.Queries;
using Identity.WebApi.Models;
using Identity.WebApi.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using static MassTransit.ValidationResultExtensions;

namespace Identity.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    [Authorize]
    public class SessionController(IMediator mediator,
        IOptions<WebApiConfiguration> options,
        ISessionStateService sessionStateService) 
        : ControllerBase
    {
        private readonly WebApiConfiguration _configuration = options.Value;

        [HttpPost("deactivate")]
        public async Task<IActionResult> DeactivateAsync(string id, CancellationToken cancellationToken)
        {
            DeactivateSessionCommand command = new()
            {
                Id = id
            };

            await mediator.Send(command, cancellationToken);

            return Ok();
        }

        [HttpGet("status")]
        [Obsolete]
        public async Task<IActionResult> GetStateAsync(CancellationToken cancellationToken)
        {
            string? refreshToken = HttpContext.Request.Cookies[_configuration.RefreshTokenName];
            SessionValidStatusDto result = await sessionStateService.GetSessionValidStatus(refreshToken, cancellationToken);

            return Ok(result);
        }
    }
}
