using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserNotifications.Applications.Commands;
using UserNotifications.WebApi.Models.Notifications;

namespace UserNotifications.WebApi.Controllers.Users
{
    [Route("api/v{version:apiVersion}/notifications")]
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
                Message = body.Message,
                Title = body.Title,
                Category = body.Category
            };
            await mediator.Send(command);

            return Ok();
        }
    }
}
