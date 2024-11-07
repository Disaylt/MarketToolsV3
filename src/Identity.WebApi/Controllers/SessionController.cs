using Asp.Versioning;
using Identity.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    [Authorize]
    public class SessionController(IMediator mediator) : ControllerBase
    {
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
    }
}
