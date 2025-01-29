using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Enums;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Commands
{
    public class CreateNotificationCommand : ICommand<Unit>
    {
        public required string UserId { get; set; }
        public required string Message { get; set; }
        public Category? Category { get; set; }
    }

    public class CreateNotificationCommandHandler(IRepository<Notification> notificationRepository) : IRequestHandler<CreateNotificationCommand, Unit>
    {
        public async Task<Unit> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            Notification entity = new Notification
            {
                Message = request.Message,
                UserId = request.UserId,
                Category = request.Category
            };

            await notificationRepository.InsertAsync(entity, cancellationToken);

            return Unit.Value;
        }
    }
}
