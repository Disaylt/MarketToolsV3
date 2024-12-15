using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WB.Seller.Api.Companies.Domain.Entities;
using WB.Seller.Api.Companies.Infrastructure.Database.EfConfigurations;

namespace WB.Seller.Api.Companies.Infrastructure.Database
{
    public class ApiCompaniesDbContext(DbContextOptions<ApiCompaniesDbContext> options) : DbContext(options)
    {
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Owner> Owners { get; set; } = null!;
        //public DbSet<Permission> Permissions { get; set; } = null!;
        //public DbSet<Subscriber> Subscribers { get; set; } = null!;
        //public DbSet<Subscription> Subscriptions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new OwnerConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
