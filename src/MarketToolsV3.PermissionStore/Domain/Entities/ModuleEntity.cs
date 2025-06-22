using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketToolsV3.PermissionStore.Domain.ValueObjects;
using MongoDB.Bson;

namespace MarketToolsV3.PermissionStore.Domain.Entities
{
    public class ModuleEntity : Entity
    {
        public required string Creator { get; set; }
        public IEnumerable<PermissionValueObject> Permissions { get; set; } = [];
    }
}
