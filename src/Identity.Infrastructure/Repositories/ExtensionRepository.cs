using Identity.Domain.Seed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repositories
{
    public class ExtensionRepository : IExtensionRepository
    {
        public async Task<int> CountAsync<T>(IQueryable<T> query) where T : class
        {
            return await query.CountAsync();
        }

        public async Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> query) where T : class
        {
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<T>> ToListAsync<T>(IQueryable<T> query) where T : class
        {
            return await query.ToListAsync();
        }
    }
}
