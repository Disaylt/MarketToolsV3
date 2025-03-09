using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserNotifications.Applications.Commands;
using UserNotifications.WebApi.Models.Notifications;

namespace UserNotifications.WebApi.Controllers.Users
{
    [Route("api/v{version:apiVersion}/notifications")]
    [ApiController]
    [ApiVersion("1")]
    public class NotificationsController(IMediator mediator)
        : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetAsync([FromQuery] GetRangeNotificationsQuery query)
        {
            CreateReadNotificationCollectionCommand request = new()
            {
                UserId = "1",
                Category = query.Category,
                IsRead = query.IsRead,
                Skip = query.Skip,
                Take = query.Take
            };

            var result = await mediator.Send(request);

            return Ok(result);
        }
    }
}
