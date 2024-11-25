using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Database;

namespace UserNotifications.Infrastructure.Repositories
{
    internal class MongoRepository<T>(IMongoCollection<T> collection,
        IClientSessionHandleContext clientSessionHandleContext,
        IUnitOfWork unitOfWork)
        : IRepository<T>
        where T : Entity
    {
        private readonly IClientSessionHandle _clientSessionHandle = clientSessionHandleContext.Session;
        public IUnitOfWork UnitOfWork => unitOfWork;

        public async Task<T> FindByIdAsync(string id, CancellationToken cancellationToken)
        {
            var filter = Builders<T>.Filter
                .Eq(restaurant => restaurant.Id, new ObjectId(id));

            return await collection.Find(filter).FirstAsync(cancellationToken);
            ;
        }

        public async Task InsertAsync(T entity, CancellationToken cancellationToken)
        {
            await collection.InsertOneAsync(_clientSessionHandle, entity, cancellationToken: cancellationToken);
        }
    }
}
