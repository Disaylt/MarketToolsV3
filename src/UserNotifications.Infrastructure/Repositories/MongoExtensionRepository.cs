using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Infrastructure.Repositories
{
    public class MongoExtensionRepository : IExtensionRepository
    {
        public Task<int> CountAsync<T>(IQueryable<T> query, CancellationToken cancellationToken)
        {
            return query.CountAsync(cancellationToken);
        }

        public Task<List<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken)
        {
            return query.ToListAsync(cancellationToken);
        }
    }
}
