using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserNotifications.Infrastructure.Database
{
    public interface IDatabaseClient<out TClient>
    {
        TClient Client { get; }
    }
}
