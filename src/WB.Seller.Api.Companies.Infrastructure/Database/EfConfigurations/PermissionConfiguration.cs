using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Api.Companies.Domain.Entities;

namespace WB.Seller.Api.Companies.Infrastructure.Database.EfConfigurations
{
    internal class PermissionConfiguration : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> builder)
        {
            builder.ToTable("permissions");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new { e.Type, e.Status, e.SubscriptionId })
                .IsUnique();

            builder.HasOne(e => e.Subscription)
                .WithMany(e => e.Permissions)
                .HasForeignKey(e => e.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
