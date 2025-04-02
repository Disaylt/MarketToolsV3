using Identity.Domain.Seed;
using Identity.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Infrastructure.Repositories
{
    internal class Repository<T>(IUnitOfWork unitOfWork, IdentityDbContext identityDbContext)
        : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet = identityDbContext.Set<T>();

        public IUnitOfWork UnitOfWork => unitOfWork;

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(entity, cancellationToken);

            return entity;
        }

        public IQueryable<T> AsQueryable()
        {
            return _dbSet.AsQueryable();
        }

        public Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Remove(entity);

            return Task.CompletedTask;
        }

        public async Task<T> FindByIdRequiredAsync(object id, CancellationToken cancellationToken)
        {
            T? entity = await _dbSet.FindAsync([id], cancellationToken: cancellationToken);

            return entity ?? throw new RootServiceException(HttpStatusCode.NotFound)
                .AddMessages($"ID: {id} not found.");
        }

        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }
    }
}
