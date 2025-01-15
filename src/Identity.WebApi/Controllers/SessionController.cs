using Asp.Versioning;
using Identity.Application.Commands;
using Identity.Application.Queries;
using Identity.WebApi.Models;
using Identity.WebApi.Services;
using Identity.WebApi.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
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
    public class SessionController(IMediator mediator, ISessionContextService sessionContextService) 
        : ControllerBase
    {
        [HttpPost("deactivate")]
        public async Task<IActionResult> DeactivateAsync(string id, CancellationToken cancellationToken)
        {
            DeactivateSessionCommand command = new()
            {
                Id = id
            };

            await mediator.Send(command, cancellationToken);
            sessionContextService.MarkAsDelete(command.Id);

            return Ok();
        }
    }
}
