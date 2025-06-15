using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.PermissionStore.Application.Models
{
    public class ActionsStoreModel<T>
    {
        public List<T> ToAdd { get; } = [];
        public List<T> ToRemove { get; } = [];
        public List<T> ToUpdate { get; } = [];
    }
}
