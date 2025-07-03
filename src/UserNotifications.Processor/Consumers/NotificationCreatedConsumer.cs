using IntegrationEvents.Contract.Profile;
using MassTransit;
using MediatR;
using UserNotifications.Applications.Commands;

namespace UserNotifications.Processor.Consumers;

public class NotificationCreatedConsumer(IMediator mediator)
    : IConsumer<CreateUserNotificationIntegrationEvent>
{
    public async Task Consume(ConsumeContext<CreateUserNotificationIntegrationEvent> context)
    {
        CreateNotificationCommand command = new()
        {
            UserId = context.Message.UserId,
            Category = context.Message.Category,
            Title = "Приветствие",
            Message = "Доброе пожаловать! Рады видеть вас. Совершенствуйте уровень управления вашего бизнеса. " +
                      "Анализируйте, собирайте статистику и будьте в курсе всех событий ваших компаний."
        };

        await mediator.Send(command);
    }
}