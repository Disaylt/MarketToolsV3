using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserNotifications.Applications.Commands;
using UserNotifications.WebApi.Models.Notifications;

namespace UserNotifications.WebApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class NotificationController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NewNotification body)
        {
            CreateNotificationCommand command = new()
            {
                UserId = body.UserId,
                Message = body.Message
            };
            await mediator.Send(command);

            return Ok();
        }
    }
}
