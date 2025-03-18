using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Database.EfConfigurations
{
    internal class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.ToTable("services");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.IdentityId)
                .HasMaxLength(100);

            builder.HasIndex(x => new { x.IdentityId, CategoryId = x.ProviderType, x.ProviderId })
                .IsUnique();

            builder.HasOne(x => x.Identity)
                .WithMany(x => x.Services)
                .HasForeignKey(e => e.IdentityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
