using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.IntegrationEventLogService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketToolsV3.IntegrationEventLogServiceEf
{
    public class IntegrationEventLogConfiguration : IEntityTypeConfiguration<IntegrationEventLogEntry>
    {
        public void Configure(EntityTypeBuilder<IntegrationEventLogEntry> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.TransactionId, x.State});
            builder.HasIndex(x => x.Type);

            builder.Property(x => x.Type).HasMaxLength(1000);
            builder.Property(x => x.Content).HasMaxLength(99999);
        }
    }
}
