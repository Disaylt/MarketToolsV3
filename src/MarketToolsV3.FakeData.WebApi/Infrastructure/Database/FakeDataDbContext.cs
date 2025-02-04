using MarketToolsV3.FakeData.WebApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure.Database
{
    public class FakeDataDbContext(DbContextOptions<FakeDataDbContext> options) : DbContext(options)
    {
        public DbSet<FakeDataTask> Tasks { get; set; } = null!;
        public DbSet<ResponseBody> Responses { get; set; } = null!;
        public DbSet<ValueUseHistory> ValueUseHistories { get; set; } = null!;
        public DbSet<TaskDetails> TasksDetails { get; set; } = null!;
    }
}
