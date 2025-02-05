using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Database.Configs
{
    public class ResponseBodyConfig : IEntityTypeConfiguration<ResponseBody>
    {
        public void Configure(EntityTypeBuilder<ResponseBody> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.Data)
                .HasMaxLength(10000);

            builder.HasOne(r => r.TaskDetails)
                .WithMany(t => t.Responses)
                .HasForeignKey(t => t.TaskDetailsId);
        }
    }
}
