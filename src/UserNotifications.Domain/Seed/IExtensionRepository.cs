using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Domain.Seed
{
    public interface IExtensionRepository
    {
        Task<List<T>> ToListAsync<T>(IQueryable<T> query);
    }
}
