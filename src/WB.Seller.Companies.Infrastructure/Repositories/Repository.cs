using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Seed;
using WB.Seller.Companies.Infrastructure.Database;

namespace WB.Seller.Companies.Infrastructure.Repositories
{
    internal class Repository<T>(IUnitOfWork unitOfWork, WbSellerCompaniesDbContext dbContext)
        : IRepository<T> 
        where T : class
    {
        private readonly DbSet<T> _dbSet = dbContext.Set<T>();

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

            return entity ?? throw new RootServiceException(HttpStatusCode.NotFound, "Not found");
        }

        public Task<T> UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            _dbSet.Update(entity);
            return Task.FromResult(entity);
        }
    }
}
