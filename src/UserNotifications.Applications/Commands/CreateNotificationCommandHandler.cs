using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Applications.Commands
{
    public class CreateNotificationCommandHandler(IRepository<Notification> notificationRepository) : IRequestHandler<CreateNotificationCommand, Unit>
    {
        public async Task<Unit> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            Notification entity = new()
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
