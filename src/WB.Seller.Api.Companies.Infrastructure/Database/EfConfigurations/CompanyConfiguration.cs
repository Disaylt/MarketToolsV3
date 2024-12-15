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
    internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.ToTable("companies");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.OwnerId);

            builder.Property(e => e.Token)
                .HasMaxLength(2000);

            builder.Property(e => e.Name)
                .HasMaxLength(100);

            builder.HasOne(e => e.Owner)
                .WithMany(e => e.Companies)
                .HasForeignKey(e => e.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(c => c.Subscribers)
                .WithMany(s => s.Companies)
                .UsingEntity<Subscription>(
                    j => j
                        .HasOne(s => s.Subscriber)
                        .WithMany(s => s.Subscriptions)
                        .HasForeignKey(s => s.SubscriberId),
                    j => j
                        .HasOne(x => x.Company)
                        .WithMany(x => x.Subscriptions)
                        .HasForeignKey(x => x.CompanyId),
                    j =>
                    {

                    });
        }
    }
}
