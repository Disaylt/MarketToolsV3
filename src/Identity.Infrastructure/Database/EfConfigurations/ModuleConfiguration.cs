using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Database.EfConfigurations
{
    internal class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable("modules");

            builder.HasKey(x => x.Id);

            builder.Property(e => e.IdentityId)
                .HasMaxLength(100);

            builder.Property(e => e.Path)
                .HasMaxLength(512);

            builder.Property(e => e.Type)
                .HasMaxLength(100);

            builder.HasIndex(x => new { x.IdentityId, x.Path, x.Type, x.ExternalId, })
                .IsUnique();

            builder.HasOne(x => x.Identity)
                .WithMany(x => x.Modules)
                .HasForeignKey(e => e.IdentityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
