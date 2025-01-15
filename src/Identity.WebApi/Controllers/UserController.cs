using Asp.Versioning;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.Application.Queries;
using Identity.WebApi.Models;
using Identity.WebApi.Services;
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
        IOptions<WebApiConfiguration> webApiConfigurationConfiguration,
        IOptions<AuthConfig> authConfigOptions,
        IAuthContext authContext)
        : ControllerBase
    {
        private readonly WebApiConfiguration _webApiConfigurationConfiguration = webApiConfigurationConfiguration.Value;
        private readonly AuthConfig _authConfigOptions = authConfigOptions.Value;

        private static readonly CookieOptions CookieOptions = new()
            { HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddYears(1) };

        [Authorize]
        [HttpPut("logout")]
        public async Task<IActionResult> LogOut(CancellationToken cancellationToken)
        {
            DeactivateSessionCommand command = new()
            {
                Id = authContext.GetSessionIdRequired()
            };
            await mediator.Send(command, cancellationToken);

            HttpContext.Response.Cookies.Delete(_webApiConfigurationConfiguration.AccessTokenName);
            HttpContext.Response.Cookies.Delete(_webApiConfigurationConfiguration.RefreshTokenName);
            HttpContext.Response.Headers.Append(_authConfigOptions.Headers.CookieAction, "new");

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

            HttpContext.Response.Cookies.Append(_webApiConfigurationConfiguration.AccessTokenName, result.AuthDetails.AuthToken, CookieOptions);
            HttpContext.Response.Cookies.Append(_webApiConfigurationConfiguration.RefreshTokenName, result.AuthDetails.SessionToken, CookieOptions);
            HttpContext.Response.Headers.Append(_configuration.AuthDetailsHeader, "new");

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

            HttpContext.Response.Cookies.Append(_webApiConfigurationConfiguration.AccessTokenName, result.AuthDetails.AuthToken, CookieOptions);
            HttpContext.Response.Cookies.Append(_webApiConfigurationConfiguration.RefreshTokenName, result.AuthDetails.SessionToken, CookieOptions);
            HttpContext.Response.Headers.Append(_configuration.AuthDetailsHeader, "new");

            return Ok(result);
        }
    }
}
