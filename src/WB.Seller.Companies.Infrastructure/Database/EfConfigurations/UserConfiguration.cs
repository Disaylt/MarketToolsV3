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
    internal class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(x => x.SubId);

            builder.Ignore(e => e.Id);

            builder.Property(e => e.Login)
                .IsRequired()
                .HasMaxLength(200);

            builder.HasMany(s => s.Companies)
                .WithMany(c => c.Users)
                .UsingEntity<SubscriptionEntity>(
                    j => j
                        .HasOne(x => x.Company)
                        .WithMany(x => x.Subscriptions)
                        .HasForeignKey(x => x.CompanyId),
                    j => j
                        .HasOne(s => s.User)
                        .WithMany(s => s.Subscriptions)
                        .HasForeignKey(s => s.UserId));
        }
    }
}
