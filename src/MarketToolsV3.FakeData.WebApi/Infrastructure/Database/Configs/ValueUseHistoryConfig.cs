using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Database.Configs
{
    public class ValueUseHistoryConfig : IEntityTypeConfiguration<ValueUseHistory>
    {
        public void Configure(EntityTypeBuilder<ValueUseHistory> builder)
        {
            builder.HasKey(h => h.Id);

            builder.Property(h => h.Path)
                .IsRequired()
                .HasMaxLength(1000);

            builder.HasOne(h => h.ResponseBody)
                .WithMany(r => r.ValueUseHistories)
                .HasForeignKey(h => h.ResponseBodyId);
        }
    }
}
