using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Identity.Infrastructure.Database;

namespace MarketToolsV3.Users.UnitTests.Mock
{
    internal static class MemoryDbContext
    {
        public static IdentityDbContext Create(string name) 
        {
            var options = new DbContextOptionsBuilder<IdentityDbContext>()
                .UseInMemoryDatabase(databaseName: name)
                .Options;

            return new(options);
        }
    }
}
