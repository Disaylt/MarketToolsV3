using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntegrationEvents.Contract;

namespace MarketToolsV3.EventBus.Models
{
    public class IntegrationEventLogEntry
    {
        private static readonly JsonSerializerOptions IndentedOptions = new() { WriteIndented = true };

        private IntegrationEventLogEntry() { }

        public IntegrationEventLogEntry(BaseIntegrationEvent @event, Guid transactionId)
        {
            Type eventType = @event.GetType();
            Content = JsonSerializer.Serialize(@event, eventType, IndentedOptions);
            TransactionId = transactionId;
            Id = @event.Id;
            Type = eventType.FullName ?? throw new NullReferenceException("Event type is null");
        }

        public Guid Id { get; private set; }
        public DateTimeOffset DateTime { get; private set; } = DateTimeOffset.UtcNow;
        public EventStateEnum State { get; set; }
        public int TimeSent { get; set; }
        public Guid TransactionId { get; private set; }
        public string Type { get; private set; }
        public string Content { get; private set; }
    }
}
