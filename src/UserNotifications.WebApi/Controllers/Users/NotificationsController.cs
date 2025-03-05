using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserNotifications.Applications.Commands;
using UserNotifications.WebApi.Models.Notifications.Users;

namespace UserNotifications.WebApi.Controllers.Users
{
    [Route("api/v{version:apiVersion}/notifications")]
    [ApiController]
    [ApiVersion("1")]
    public class NotificationsController(IMediator mediator)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetRangeNotificationsQuery query)
        {
            

            return Ok();
        }
    }
}
