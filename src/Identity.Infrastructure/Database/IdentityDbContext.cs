using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Infrastructure.Database.EfConfigurations;
using MarketToolsV3.IntegrationEventLogService.Models;
using MarketToolsV3.IntegrationEventLogServiceEf;
using MassTransit;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Database
{
    public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<IdentityPerson>(options)
    {
        public DbSet<Session> Sessions { get; set; } = null!;
        public DbSet<Module> Modules { get; set; } = null!;
        public DbSet<ModuleRole> ModuleRoles { get; set; } = null!;
        public DbSet<ModuleClaim> ModuleClaims { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SessionsConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleClaimConfiguration());
            modelBuilder.ApplyConfiguration(new ModuleRoleConfiguration());

            modelBuilder.AddInboxStateEntity();
            modelBuilder.AddOutboxMessageEntity();
            modelBuilder.AddOutboxStateEntity();

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("public");
        }
    }
}
