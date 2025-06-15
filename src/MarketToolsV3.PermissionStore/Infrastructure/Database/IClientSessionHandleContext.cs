using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.PermissionStore.Infrastructure.Database
{
    public interface IClientSessionHandleContext
    {
        IClientSessionHandle Session { get; }
    }
}
