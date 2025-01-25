using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserNotifications.Domain.Entities;
using UserNotifications.Domain.Seed;
using UserNotifications.Domain.UpdateDetails;

namespace UserNotifications.Infrastructure.Utilities.Mongo.UpdateDifinition.Builders
{
    internal class MongoUpdateDifinitionBuilder(INotificationUpdateDetails notificationUpdateDetails)
    {
        private static readonly UpdateDefinitionBuilder<Notification> _updateBuilder = Builders<Notification>.Update;
        private readonly Dictionary<string, UpdateDefinition<Notification>> _propertyNameAndDefinitionPairs = new();

        public MongoUpdateDifinitionBuilder UseIsRead()
        {
            if (notificationUpdateDetails.IsRead.HasValue == false)
            {
                return this;
            }

            string key = nameof(notificationUpdateDetails.IsRead);
            UpdateDefinition<Notification> value = _updateBuilder.Set(n => n.IsRead, notificationUpdateDetails.IsRead.Value);

            if (!_propertyNameAndDefinitionPairs.TryAdd(key, value))
            {
                _propertyNameAndDefinitionPairs[key] = value;
            }

            return this;
        }

        public UpdateDefinition<Notification> Build()
        {
            if(_propertyNameAndDefinitionPairs.Count == 0)
            {
                throw new RootServiceException(System.Net.HttpStatusCode.BadRequest, "Необходимо добавить хотя бы одно значение для изменения.");
            }

            return _updateBuilder.Combine(_propertyNameAndDefinitionPairs.Values);
        }
    }
}
