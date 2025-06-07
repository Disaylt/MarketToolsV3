using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MarketToolsV3.PermissionStore.Domain.Entities
{
    public class PermissionEntity : Entity
    {
        public required string Module { get; set; }
        public required string Path { get; set; }
    }
}
