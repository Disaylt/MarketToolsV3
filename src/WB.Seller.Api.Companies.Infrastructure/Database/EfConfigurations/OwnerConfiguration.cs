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
    internal class OwnerConfiguration : IEntityTypeConfiguration<Owner>
    {
        public void Configure(EntityTypeBuilder<Owner> builder)
        {
            builder.ToTable("owners");

            builder.HasKey(x => x.UserId);

            builder.Ignore(e => e.Id);

            builder.HasMany(e => e.Companies)
                .WithOne(e=> e.Owner)
                .HasForeignKey(e=> e.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
