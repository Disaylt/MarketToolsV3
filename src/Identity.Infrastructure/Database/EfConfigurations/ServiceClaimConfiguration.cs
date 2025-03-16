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
    public class ServiceClaimConfiguration : IEntityTypeConfiguration<ServiceClaim>
    {
        public void Configure(EntityTypeBuilder<ServiceClaim> builder)
        {
            builder.ToTable("serviceClaims");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.ServiceId, x.Type })
                .IsUnique();

            builder.HasOne(x => x.Service)
                .WithMany(x => x.Claims)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
