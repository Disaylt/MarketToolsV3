﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Domain.Seed
{
    public interface IExtensionRepository
    {
        Task<List<T>> ToListAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) where T : class;
        Task<int> CountAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) where T : class;
        Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> query, CancellationToken cancellationToken) where T : class;
    }
}
