using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.PermissionStore.Domain.Entities;

namespace MarketToolsV3.PermissionStore.Domain.Seed
{
    public interface IRepository<T> where T : Entity
    {
        ITransactionContext UnitOfWork { get; }
        Task<T> FindByIdAsync(string id, CancellationToken cancellationToken);
        Task InsertAsync(T entity, CancellationToken cancellationToken);
        Task InsertManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        Task UpdateAsync(ModuleEntity entity, CancellationToken cancellationToken);
        Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
        IQueryable<T> AsQueryable();
    }
}
