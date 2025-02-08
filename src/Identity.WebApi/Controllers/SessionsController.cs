using Asp.Versioning;
using Identity.Application.Models;
using Identity.Application.Queries;
using Identity.WebApi.Models;
using Identity.WebApi.Services;
using Identity.WebApi.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    [Authorize]
    public class SessionsController(
        IMediator mediator, 
        IAuthContext authContext,
        ISessionViewMapper sessionViewMapper)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync(CancellationToken cancellationToken)
        {
            GetActiveSessionsQuery query = new()
            {
                CurrentSessionId = authContext.SessionId,
                UserId = authContext.GetUserIdRequired()
            };

            IEnumerable<SessionDto> sessions = await mediator.Send(query, cancellationToken);

            SessionListViewModel viewResult = new()
            {
                CurrentSessionId = authContext.SessionId ?? string.Empty,
                Sessions = sessions
                    .Select(sessionViewMapper.MapFrom)
                    .ToList()
            };

            return Ok(viewResult);
        }





    }
}
