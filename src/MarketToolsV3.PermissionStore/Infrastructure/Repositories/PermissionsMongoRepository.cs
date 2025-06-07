using MarketToolsV3.PermissionStore.Domain.Entities;
using MarketToolsV3.PermissionStore.Domain.Seed;
using MarketToolsV3.PermissionStore.Infrastructure.Database;
using MongoDB.Driver;

namespace MarketToolsV3.PermissionStore.Infrastructure.Repositories;

public class PermissionsMongoRepository(
    IMongoCollection<PermissionEntity> collection, 
    IClientSessionHandleContext clientSessionHandleContext, 
    ITransactionContext unitOfWork) 
    : MongoRepository<PermissionEntity>(collection, clientSessionHandleContext, unitOfWork)
{
    public override async Task UpdateRangeAsync(IEnumerable<PermissionEntity> entities, CancellationToken cancellationToken)
    {
        var bulkOps = new List<WriteModel<PermissionEntity>>();

        foreach (var entity in entities)
        {
            var filter = Builders<PermissionEntity>.Filter.Eq(x => x.Id, entity.Id);
            var update = Builders<PermissionEntity>.Update
                .Set(x => x.Module, entity.Module);

            bulkOps.Add(new UpdateOneModel<PermissionEntity>(filter, update));
        }

        await Collection.BulkWriteAsync(ClientSessionHandle, bulkOps, null, cancellationToken);
    }
}