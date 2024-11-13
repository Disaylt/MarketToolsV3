using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace UserNotifications.Domain.Entities
{
    public class Entity
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}
