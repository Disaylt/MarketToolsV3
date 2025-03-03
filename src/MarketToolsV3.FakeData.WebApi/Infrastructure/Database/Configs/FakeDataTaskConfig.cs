using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Database.Configs
{
    public class FakeDataTaskConfig : IEntityTypeConfiguration<FakeDataTaskEntity>
    {
        public void Configure(EntityTypeBuilder<FakeDataTaskEntity> builder)
        {
            builder.HasKey(e => e.TaskId);
            builder.Property(e => e.TaskId)
                .HasMaxLength(1000);

            builder.Ignore(x => x.Id);
        }
    }
}
