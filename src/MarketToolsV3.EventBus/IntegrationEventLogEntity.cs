using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using IntegrationEvents.Contract;

namespace MarketToolsV3.EventBus
{
    public class IntegrationEventLogEntity
    {
        private static readonly JsonSerializerOptions IndentedOptions = new() { WriteIndented = true };

#pragma warning disable CS8618, CS9264
        /// <summary>
        /// For EF core
        /// </summary>
        private IntegrationEventLogEntity(){}
#pragma warning restore CS8618, CS9264

        public IntegrationEventLogEntity(BaseIntegrationEvent @event, Guid transactionId)
        {
            Content = JsonSerializer.Serialize(@event, @event.GetType(), IndentedOptions);
            TransactionId = transactionId;
            Id = @event.Id;
            Type = @event.GetType().FullName 
                   ?? throw new NullReferenceException("Event type is null");
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
