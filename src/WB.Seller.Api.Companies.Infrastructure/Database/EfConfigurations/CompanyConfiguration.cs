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

            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.OwnerId);

            builder.Property(c => c.Token)
                .HasMaxLength(2000);

            builder.Property(c => c.Name)
                .HasMaxLength(100);
        }
    }
}
