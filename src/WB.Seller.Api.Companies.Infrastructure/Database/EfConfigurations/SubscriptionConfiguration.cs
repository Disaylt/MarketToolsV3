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
    internal class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.ToTable("subscriptions");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => new { e.CompanyId, e.SubscriberId })
                .IsUnique();

            builder.HasMany(e => e.Permissions)
                .WithOne(e => e.Subscription)
                .HasForeignKey(e => e.SubscriptionId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Company)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(e => e.CompanyId);

            builder.HasOne(e => e.Subscriber)
                .WithMany(e => e.Subscriptions)
                .HasForeignKey(e => e.SubscriberId);
        }
    }
}
