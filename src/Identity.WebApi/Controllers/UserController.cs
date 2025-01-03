using Asp.Versioning;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Identity.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class UserController(IMediator mediator, 
        IOptions<WebApiConfiguration> options)
        : ControllerBase
    {
        private readonly WebApiConfiguration _configuration = options.Value;

        private static readonly CookieOptions _cookieOptions = new()
            { HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddYears(1) };

        [Authorize]
        [HttpGet("details")]
        public async Task<IActionResult> GetDetailsAsync()
        {
            return Ok();
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

            AuthDetailsDto result = await mediator.Send(command, cancellationToken);

            HttpContext.Response.Cookies.Append(_configuration.AccessTokenName, result.AuthToken, _cookieOptions);
            HttpContext.Response.Cookies.Append(_configuration.RefreshTokenName, result.SessionToken, _cookieOptions);

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

            AuthDetailsDto result = await mediator.Send(command, cancellationToken);

            HttpContext.Response.Cookies.Append(_configuration.AccessTokenName, result.AuthToken, _cookieOptions);
            HttpContext.Response.Cookies.Append(_configuration.RefreshTokenName, result.SessionToken, _cookieOptions);

            return Ok(result);
        }
    }
}
