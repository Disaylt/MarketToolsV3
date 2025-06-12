using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MarketToolsV3.PermissionStore.Infrastructure.Database;
using MongoDB.Driver;

namespace MarketToolsV3.PermissionStore.Infrastructure.Repositories;

public abstract class MongoRepository<T>(IMongoCollection<T> collection,
    IClientSessionHandleContext clientSessionHandleContext,
    ITransactionContext unitOfWork) : IRepository<T>
    where T : Entity
{
    protected IClientSessionHandle ClientSessionHandle { get; } = clientSessionHandleContext.Session;
    protected IMongoCollection<T> Collection { get; } = collection;
    public ITransactionContext UnitOfWork { get; } = unitOfWork;

    public IQueryable<T> AsQueryable()
    {
        return Collection.AsQueryable();
    }

    public async Task<T> FindByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<T>.Filter
            .Eq(restaurant => restaurant.Id, id);

        return await Collection.Find(filter).FirstAsync(cancellationToken);
    }

    public async Task InsertAsync(T entity, CancellationToken cancellationToken)
    {
        await Collection.InsertOneAsync(ClientSessionHandle, entity, cancellationToken: cancellationToken);
    }

    public async Task InsertManyAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        await Collection.InsertManyAsync(ClientSessionHandle, entities, cancellationToken: cancellationToken);
    }

    public async Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken)
    {
        IEnumerable<string> idsToDelete = entities.Select(e => e.Id);

        await Collection.DeleteManyAsync(ClientSessionHandle, x=> idsToDelete.Contains(x.Id), cancellationToken: cancellationToken);
    }

    public abstract Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken);
    public abstract Task UpdateAsync(ModuleEntity entity, CancellationToken cancellationToken);
}