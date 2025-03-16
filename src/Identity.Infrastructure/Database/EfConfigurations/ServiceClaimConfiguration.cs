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

            builder.HasOne(x => x.Service)
                .WithMany(x => x.Claims)
                .HasForeignKey(e => new { e.CategoryId, e.ProviderId })
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
