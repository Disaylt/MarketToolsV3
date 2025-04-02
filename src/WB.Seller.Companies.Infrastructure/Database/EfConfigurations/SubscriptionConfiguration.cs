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
    internal class SubscriptionConfiguration : BaseConfiguration<SubscriptionEntity>
    {
        public override void Configure(EntityTypeBuilder<SubscriptionEntity> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new { e.CompanyId, SubscriberId = e.UserId })
                .IsUnique();

            builder.Property(e => e.Note)
                .HasMaxLength(500);

            builder.HasMany(e => e.Permissions)
                .WithOne(e => e.Subscription)
                .HasForeignKey(e => e.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Company)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(e => e.CompanyId);

            builder.HasOne(e => e.User)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(e => e.UserId);
        }
    }
}
