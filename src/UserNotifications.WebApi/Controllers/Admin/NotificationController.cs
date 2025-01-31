using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserNotifications.Applications.Commands;
using UserNotifications.WebApi.Models.Notifications.Admin;

namespace UserNotifications.WebApi.Controllers.Admin
{
    [Route("api/v{version:apiVersion}/admin/[controller]")]
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
