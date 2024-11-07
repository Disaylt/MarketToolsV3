using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace MarketToolsV3.Users.IntegrationTests.Source
{
    internal class MemoryDbContextFactory
    {
        public static IdentityDbContext CreateIdentityContext(string? name = null)
        {
            DbContextOptions<IdentityDbContext> options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: name ?? Guid.NewGuid().ToString())
                .Options;

            return new IdentityDbContext(options);
        }
    }
}
