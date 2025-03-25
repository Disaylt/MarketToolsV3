using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WB.Seller.Companies.Domain.Seed;

namespace WB.Seller.Companies.Infrastructure.Database
{
    public class EfCoreUnitOfWork<TContext>(TContext context) : IUnitOfWork, IDisposable, IAsyncDisposable
       where TContext : DbContext
    {
        private IDbContextTransaction? _transaction;
        public bool HasTransaction => _transaction != null;

        public Guid CurrentTransactionId
        {
            get
            {
                if (_transaction == null)
                {
                    throw new NullReferenceException("Transaction not begin");
                }

                return _transaction.TransactionId;
            }
        }
        public virtual async Task<Guid> BeginTransactionAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction != null)
            {
                return _transaction.TransactionId;
            }

            _transaction = await context.Database.BeginTransactionAsync(cancellationToken);

            return _transaction.TransactionId;
        }

        public virtual async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await context.SaveChangesAsync(cancellationToken);

            return true;
        }

        public virtual async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null) throw new NullReferenceException(nameof(_transaction));

            try
            {
                await context.SaveChangesAsync(cancellationToken);
                await _transaction.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackAsync(cancellationToken);
                throw;
            }
            finally
            {
                DisposeTransaction();
            }
        }

        public virtual async Task RollbackAsync(CancellationToken cancellationToken = default)
        {
            if (_transaction == null) return;

            try
            {
                await _transaction.RollbackAsync(cancellationToken);
            }
            finally
            {
                DisposeTransaction();
            }
        }


        public virtual void Dispose()
        {
            DisposeTransaction();
            GC.SuppressFinalize(this);
        }

        public virtual ValueTask DisposeAsync()
        {
            DisposeTransaction();
            GC.SuppressFinalize(this);

            return ValueTask.CompletedTask;
        }

        private void DisposeTransaction()
        {
            if (_transaction == null) return;
            _transaction.Dispose();
            _transaction = null;
        }

        public virtual Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return context.SaveChangesAsync(cancellationToken);
        }
    }
}
