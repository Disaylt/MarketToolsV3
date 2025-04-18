﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WB.Seller.Companies.Domain.Seed
{
    public interface IRepository<T> where T : class
    {
        IUnitOfWork UnitOfWork { get; }
        IQueryable<T> AsQueryable();
        Task<T> FindByIdRequiredAsync(object id, CancellationToken cancellationToken);
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
    }
}
