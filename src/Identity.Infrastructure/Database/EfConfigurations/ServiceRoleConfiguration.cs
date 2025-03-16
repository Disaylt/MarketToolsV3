using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Database.EfConfigurations
{
    internal class ServiceRoleConfiguration : IEntityTypeConfiguration<ServiceRole>
    {
        public void Configure(EntityTypeBuilder<ServiceRole> builder)
        {
            builder.ToTable("serviceRoles");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => new { x.ServiceId, x.Value })
                .IsUnique();

            builder.HasOne(x => x.Service)
                .WithMany(x => x.Roles)
                .HasForeignKey(e => e.ServiceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
