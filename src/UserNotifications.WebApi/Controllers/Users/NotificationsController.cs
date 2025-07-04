using Asp.Versioning;
using MarketToolV3.Authentication.Services.Abstract;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserNotifications.Applications.Commands;
using UserNotifications.Applications.Queries;
using UserNotifications.Applications.Utilities.Abstract;
using UserNotifications.WebApi.Models.Notifications;

namespace UserNotifications.WebApi.Controllers.Users
{
    [Route("api/v{version:apiVersion}/notifications")]
    [ApiController]
    [ApiVersion(1)]
    [Authorize]
    public class NotificationsController(IMediator mediator,
        IAuthContext authContext)
        : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion(1)]
        public async Task<IActionResult> GetAsync([FromQuery] GetRangeNotificationsQuery query)
        {
            CreateReadNotificationCollectionCommand request = new()
            {
                UserId = authContext.GetUserIdRequired(),
                Category = query.Category,
                IsRead = query.IsRead,
                Skip = query.Skip,
                Take = query.Take
            };

            var result = await mediator.Send(request);

            return Ok(result);
        }

        [HttpGet("count-new")]
        [MapToApiVersion(1)]
        public async Task<IActionResult> CountNewAsync()
        {
            CountNewNotificationsQuery request = new()
            {
                UserId = authContext.GetUserIdRequired()
            };

            var result = await mediator.Send(request);

            return Ok(new { Count = result });
        }

        [HttpGet("categories")]
        [MapToApiVersion(1)]
        public IActionResult GetCategories()
        {
            return Ok();
        }
    }
}
