using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserNotifications.Applications.Commands;
using UserNotifications.WebApi.Models.Notifications.Users;

namespace UserNotifications.WebApi.Controllers.Users
{
    [Route("api/v{version:apiVersion}/users/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class NotificationsController(IMediator mediator)
        : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] GetRangeNotificationsQuery query)
        {
            CreateNotificationsListForUserCommand request = new()
            {
                Take = query.Take,
                Skip = query.Skip,
                UserId = "1"
            };

            var response = await mediator.Send(request);

            return Ok(response);
        }
    }
}
