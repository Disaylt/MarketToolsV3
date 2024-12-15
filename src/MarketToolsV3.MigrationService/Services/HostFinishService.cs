using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MarketToolsV3.MigrationService.Services
{
    internal class HostFinishService(IOptions<HostCounterConfig> options,
        IHostApplicationLifetime hostApplicationLifetime)
    {
        private readonly HostCounterConfig _config = options.Value;
        private readonly object _lock = new ();

        public void MarkAsComplete()
        {
            lock (_lock)
            {
                if (_config.Quantity == 1)
                {
                    hostApplicationLifetime.StopApplication();
                }
                else
                {
                    _config.Quantity -= 1;
                }
            }
        }
    }
}
