using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Infrastructure.Database
{
    public class MongoUnitOfWork(IClientSessionHandleContext sessionContext)
        : IUnitOfWork
    {
        private readonly IClientSessionHandle _sessionHandle = sessionContext.Session;

        public bool HasTransaction => _sessionHandle.IsInTransaction;

        public Task BeginTransactionAsync()
        {
            if (HasTransaction == false)
            {
                _sessionHandle.StartTransaction();
            }

            return Task.CompletedTask;
        }

        public async Task CommitTransactionAsync()
        {
            await _sessionHandle.CommitTransactionAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            await _sessionHandle.AbortTransactionAsync();
        }
    }
}
