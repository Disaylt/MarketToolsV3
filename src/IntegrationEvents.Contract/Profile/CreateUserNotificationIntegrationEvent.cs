namespace IntegrationEvents.Contract.Profile;

public record CreateUserNotificationIntegrationEvent : BaseIntegrationEvent
{
    public required string UserId { get; set; }
    public required string Message { get; set; }
    public string? Title { get; set; }
    public required string Category { get; set; }
}