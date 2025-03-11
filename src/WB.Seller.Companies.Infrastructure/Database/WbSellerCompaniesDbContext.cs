using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Entities;
using WB.Seller.Companies.Infrastructure.Database.EfConfigurations;

namespace WB.Seller.Companies.Infrastructure.Database
{
    public class WbSellerCompaniesDbContext(DbContextOptions<WbSellerCompaniesDbContext> options) : DbContext(options)
    {
        public DbSet<CompanyEntity> Companies { get; set; } = null!;
        public DbSet<PermissionEntity> Permissions { get; set; } = null!;
        public DbSet<UserEntity> Users { get; set; } = null!;
        public DbSet<SubscriptionEntity> Subscriptions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompanyConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
