﻿using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database.Configs;
using Microsoft.EntityFrameworkCore;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Database
{
    public class FakeDataDbContext(DbContextOptions<FakeDataDbContext> options) : DbContext(options)
    {
        public DbSet<FakeDataTask> Tasks { get; set; } = null!;
        public DbSet<ResponseBody> Responses { get; set; } = null!;
        public DbSet<TaskDetails> TasksDetails { get; set; } = null!;
        public DbSet<CookieEntity> Cookies { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new FakeDataTaskConfig());
            modelBuilder.ApplyConfiguration(new TaskDetailsConfig());
            modelBuilder.ApplyConfiguration(new ResponseBodyConfig());
            modelBuilder.ApplyConfiguration(new CookieConfig());

            base.OnModelCreating(modelBuilder);
        }
    }
}
