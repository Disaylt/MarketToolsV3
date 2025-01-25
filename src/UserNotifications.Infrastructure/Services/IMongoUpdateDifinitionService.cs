using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;

namespace UserNotifications.Infrastructure.Services
{
    internal interface IMongoUpdateDifinitionService<TUpdate, TEntity> 
        where TUpdate : IUpdateDetails<TEntity>
        where TEntity : Entity
    {
        public UpdateDefinition<TEntity> Create(TUpdate update);
    }
}
