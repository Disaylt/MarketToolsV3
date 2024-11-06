using Asp.Versioning;
using Identity.Application.Models;
using Identity.Application.Queries;
using Identity.WebApi.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    [Authorize]
    public class SessionsController(IMediator mediator, IAuthContext authContext)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            GetActiveSessionsQuery query = new GetActiveSessionsQuery
            {
                CurrentSessionId = authContext.SessionId,
                UserId = authContext.GetUserIdRequired()
            };

            IEnumerable<SessionDto> sessions = await mediator.Send(query);

            return Ok(sessions);
        }
    }
}
