using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MarketToolsV3.PermissionStore.Infrastructure.Database;
using MongoDB.Driver;

namespace MarketToolsV3.PermissionStore.Infrastructure.Repositories;

public class PermissionsMongoRepository(
    IMongoCollection<ModuleEntity> collection, 
    IClientSessionHandleContext clientSessionHandleContext, 
    ITransactionContext unitOfWork) 
    : MongoRepository<ModuleEntity>(collection, clientSessionHandleContext, unitOfWork)
{
    public override async Task UpdateRangeAsync(IEnumerable<ModuleEntity> entities, CancellationToken cancellationToken)
    {
        var bulkOps = new List<WriteModel<ModuleEntity>>();

        foreach (var entity in entities)
        {
            var filter = Builders<ModuleEntity>.Filter.Eq(x => x.Id, entity.Id);
            var update = CreateUpdateDefinition(entity);

            bulkOps.Add(new UpdateOneModel<ModuleEntity>(filter, update));
        }

        await Collection.BulkWriteAsync(ClientSessionHandle, bulkOps, null, cancellationToken);
    }

    public override async Task UpdateAsync(ModuleEntity entity, CancellationToken cancellationToken)
    {
        var filter = Builders<ModuleEntity>.Filter.Eq(x => x.Id, entity.Id);
        var update = CreateUpdateDefinition(entity);

        await Collection.UpdateOneAsync(filter, update, null, cancellationToken);
    }

    private UpdateDefinition<ModuleEntity> CreateUpdateDefinition(ModuleEntity entity)
    {
        return Builders<ModuleEntity>.Update
            .Set(x => x.Permissions, entity.Permissions)
            .Set(x => x.Permissions, entity.Permissions);
    }
}