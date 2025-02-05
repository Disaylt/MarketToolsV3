using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Database.Configs
{
    public class TaskDetailsConfig : IEntityTypeConfiguration<TaskDetails>
    {
        public void Configure(EntityTypeBuilder<TaskDetails> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Path)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(t => t.Tag)
                .HasMaxLength(100);

            builder.Property(t => t.JsonBody)
                .HasMaxLength(10000);

            builder.Property(t => t.TaskId)
                .HasMaxLength(100);

            builder.Property(t => t.Method)
                .HasMaxLength(50);

            builder.HasOne(t => t.Task)
                .WithMany(t => t.Details)
                .HasForeignKey(t => t.TaskId);
        }
    }
}
