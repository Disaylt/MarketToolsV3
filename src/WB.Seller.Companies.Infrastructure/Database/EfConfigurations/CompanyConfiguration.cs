using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Entities;

namespace WB.Seller.Companies.Infrastructure.Database.EfConfigurations
{
    internal class CompanyConfiguration : BaseConfiguration<CompanyEntity>
    {
        public override void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            base.Configure(builder);

            builder.HasKey(e => e.Id);

            builder.Property(e => e.Token)
                .HasMaxLength(2000);

            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.HasMany(c => c.Users)
                .WithMany(s => s.Companies)
                .UsingEntity<SubscriptionEntity>(
                    j => j
                        .HasOne(s => s.User)
                        .WithMany(s => s.Subscriptions)
                        .HasForeignKey(s => s.UserId),
                    j => j
                        .HasOne(x => x.Company)
                        .WithMany(x => x.Subscriptions)
                        .HasForeignKey(x => x.CompanyId));

        }
    }
}
