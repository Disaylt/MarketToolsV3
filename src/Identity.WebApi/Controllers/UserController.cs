using Asp.Versioning;
using Identity.Application.Commands;
using Identity.Application.Models;
using Identity.WebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Identity.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class UserController(IMediator mediator, IOptions<WebApiConfiguration> options)
        : ControllerBase
    {
        private readonly WebApiConfiguration _configuration = options.Value;

        private static CookieOptions _cookieOptions = new CookieOptions
            { HttpOnly = true, Expires = DateTimeOffset.UtcNow.AddYears(1) };

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] NewUserModel user, CancellationToken cancellationToken)
        {
            CreateNewUserCommand command = new CreateNewUserCommand
            {
                Email = user.Email,
                Login = user.Login,
                Password = user.Password
            };

            AuthDetailsDto result = await mediator.Send(command, cancellationToken);

            HttpContext.Response.Cookies.Append(_configuration.AccessTokenName, result.AuthToken, _cookieOptions);
            HttpContext.Response.Cookies.Append(_configuration.RefreshTokenName, result.SessionToken, _cookieOptions);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginModel body, CancellationToken cancellationToken)
        {
            LoginCommand command = new LoginCommand
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
