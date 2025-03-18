using System.Net;
using Asp.Versioning;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Queries;
using Identity.Domain.Seed;
using Identity.WebApi.Models;
using Identity.WebApi.Services;
using Identity.WebApi.Services.Implementation;
using Identity.WebApi.Services.Interfaces;
using MarketToolsV3.ConfigurationManager.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static MassTransit.ValidationResultExtensions;

namespace Identity.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/users")]
    [ApiController]
    [ApiVersion("1")]
    public class UserController(IMediator mediator, 
        ICredentialsService credentialsService,
        IAuthContext authContext)
        : ControllerBase
    {
        [Authorize]
        [HttpPut("logout")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> LogOut(CancellationToken cancellationToken)
        {
            DeactivateSessionCommand command = new()
            {
                Id = authContext.GetSessionIdRequired(),
                UserId = authContext.GetUserIdRequired()
            };
            await mediator.Send(command, cancellationToken);
            credentialsService.Remove(command.Id);

            return Ok();
        }

        [Authorize]
        [HttpGet("details")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetDetailsAsync(CancellationToken cancellationToken)
        {
            GetIdentityDetailsQuery query = new()
            {
                UserId = authContext.GetUserIdRequired()
            };

            IdentityDetailsDto result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost("register")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> RegisterAsync([FromBody] NewUserModel user, CancellationToken cancellationToken)
        {
            CreateNewUserCommand command = new()
            {
                Email = user.Email,
                Login = user.Login,
                Password = user.Password,
                UserAgent = Request.Headers.UserAgent.FirstOrDefault() ?? "Неизвестное устройство"
            };

            AuthResultDto result = await mediator.Send(command, cancellationToken);

            credentialsService.Refresh(result.AuthDetails.AuthToken, result.AuthDetails.SessionToken);

            return Ok(result);
        }

        [HttpPost("login")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel body, CancellationToken cancellationToken)
        {
            LoginCommand command = new()
            {
                Email = body.Email,
                Password = body.Password,
                UserAgent = Request.Headers.UserAgent.FirstOrDefault() ?? "Неизвестное устройство"
            };

            AuthResultDto result = await mediator.Send(command, cancellationToken);

            credentialsService.Refresh(result.AuthDetails.AuthToken, result.AuthDetails.SessionToken);

            return Ok(result);
        }
    }
}
