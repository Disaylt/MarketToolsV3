using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Entities;

namespace WB.Seller.Companies.Infrastructure.Database.EfConfigurations
{
    internal class PermissionConfiguration : BaseConfiguration<PermissionEntity>
    {
        public override void Configure(EntityTypeBuilder<PermissionEntity> builder)
        {
            base.Configure(builder);

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
