﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Domain.Entities;
using Identity.Infrastructure.Database.EfConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Database
{
    public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<IdentityPerson>(options)
    {
        public DbSet<Session> Sessions { get; set; } = null!;
        public DbSet<Service> Services { get; set; } = null!;
        public DbSet<ServiceRole> ServiceRoles { get; set; } = null!;
        public DbSet<ServiceClaim> ServiceClaims { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SessionsConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceClaimConfiguration());
            modelBuilder.ApplyConfiguration(new ServiceRoleConfiguration());

            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("public");
        }
    }
}
