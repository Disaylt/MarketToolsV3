using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Domain.Seed
{
    public interface IRepository<T> where T : Entity
    {
        IUnitOfWork UnitOfWork { get; }
        Task<T> FindByIdAsync(string id, CancellationToken cancellationToken);
        Task InsertAsync(T entity, CancellationToken cancellationToken);
    }
}
