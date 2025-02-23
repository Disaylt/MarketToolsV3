using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Database.Configs
{
    public class CookieConfig : IEntityTypeConfiguration<CookieEntity>
    {
        public void Configure(EntityTypeBuilder<CookieEntity> builder)
        {
            builder.Property(e => e.Name)
                .HasMaxLength(100);
            builder.Property(e => e.Path)
                .HasMaxLength(100);
            builder.Property(e => e.Domain)
                .HasMaxLength(100);
            builder.Property(e => e.TaskId)
                .HasMaxLength(100);
            builder.Property(e => e.Value)
                .HasMaxLength(4096);

            builder.HasOne(e => e.Task)
                .WithMany(e => e.Cookies)
                .HasForeignKey(e => e.TaskId);
        }
    }
}
