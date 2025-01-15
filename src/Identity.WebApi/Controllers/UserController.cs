using Asp.Versioning;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Queries;
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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class UserController(IMediator mediator, 
        ICookiesContextService cookiesContextService,
        ICredentialsService credentialsService,
        IAuthContext authContext)
        : ControllerBase
    {
        [Authorize]
        [HttpPut("logout")]
        public async Task<IActionResult> LogOut(CancellationToken cancellationToken)
        {
            DeactivateSessionCommand command = new()
            {
                Id = authContext.GetSessionIdRequired()
            };
            await mediator.Send(command, cancellationToken);

            cookiesContextService.DeleteAccessToken();
            cookiesContextService.DeleteSessionToken();
            cookiesContextService.MarkAsNew();

            return Ok();
        }

        [Authorize]
        [HttpGet("details")]
        public async Task<IActionResult> GetDetailsAsync(CancellationToken cancellationToken)
        {
            GetIdentityDetailsQuery query = new GetIdentityDetailsQuery
            {
                UserId = authContext.GetUserIdRequired()
            };

            IdentityDetailsDto result = await mediator.Send(query, cancellationToken);

            return Ok(result);
        }

        [HttpPost("register")]
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

            credentialsService.RefreshCredentials(result.AuthDetails.AuthToken, result.AuthDetails.SessionToken);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel body, CancellationToken cancellationToken)
        {
            LoginCommand command = new()
            {
                Email = body.Email,
                Password = body.Password,
                UserAgent = Request.Headers.UserAgent.FirstOrDefault() ?? "Неизвестное устройство"
            };

            AuthResultDto result = await mediator.Send(command, cancellationToken);

            credentialsService.RefreshCredentials(result.AuthDetails.AuthToken, result.AuthDetails.SessionToken);

            return Ok(result);
        }
    }
}
