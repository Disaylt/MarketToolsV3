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
    internal class ModuleRoleConfiguration : IEntityTypeConfiguration<ModuleRole>
    {
        public void Configure(EntityTypeBuilder<ModuleRole> builder)
        {
            builder.ToTable("moduleRoles");

            builder.HasKey(x => new { x.Value, x.ModuleId});

            builder.Property(x => x.Value)
                .HasMaxLength(100);

            builder.HasOne(x => x.Module)
                .WithMany(x => x.Roles)
                .HasForeignKey(e => e.ModuleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
