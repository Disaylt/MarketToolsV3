using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Infrastructure.Database.EfConfigurations
{
    internal class BaseConfiguration<T> : IEntityTypeConfiguration<T>
        where T : Entity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Ignore(x => x.DomainEvents);
        }
    }
}
