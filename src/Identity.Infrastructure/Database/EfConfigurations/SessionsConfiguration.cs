using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Database.EfConfigurations
{
    internal class SessionsConfiguration : IEntityTypeConfiguration<Session>
    {
        public void Configure(EntityTypeBuilder<Session> builder)
        {
            builder.ToTable("sessions");

            builder.HasKey(e => e.Id);

            builder.HasIndex(e => e.IdentityId);

            builder.Property(e => e.Token)
                .HasMaxLength(1000);

            builder.Property(e => e.UserAgent)
                .HasMaxLength(1000);

            builder.HasOne(x => x.Identity)
                .WithMany(x => x.Sessions)
                .HasForeignKey(e => e.IdentityId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
