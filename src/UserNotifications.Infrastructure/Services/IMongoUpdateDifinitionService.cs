using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.NewFieldsData;

namespace UserNotifications.Infrastructure.Services
{
    internal interface IMongoUpdateDifinitionService<TUpdate, TEntity> 
        where TUpdate : INewFieldsData<TEntity>
        where TEntity : Entity
    {
        public UpdateDefinition<TEntity> Create(TUpdate update);
    }
}
