using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Database.EfConfigurations
{
    public class ModuleClaimConfiguration : IEntityTypeConfiguration<ModuleClaim>
    {
        public void Configure(EntityTypeBuilder<ModuleClaim> builder)
        {
            builder.ToTable("moduleClaims");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.ModuleId, x.Type })
                .IsUnique();

            builder.Property(x => x.Type)
                .HasMaxLength(200);

            builder.HasOne(x => x.Module)
                .WithMany(x => x.Claims)
                .HasForeignKey(e => e.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
