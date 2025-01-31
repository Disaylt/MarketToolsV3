using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;

namespace UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition
{
    public interface IMongoUpdateDifinitionBuilder<T> where T : Entity
    {
        UpdateDefinition<T> Build();
    }
}
