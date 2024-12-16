using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MarketToolsV3.MigrationService.Services
{
    internal class MigrationTaskService(IOptions<HostCounterConfig> options) : IMigrationTaskService
    {
        private readonly HostCounterConfig _config = options.Value;
        private readonly object _lock = new ();

        public event Action? CompletedAllTask;

        public void MarkAsComplete()
        {
            lock (_lock)
            {
                if (_config.Quantity == 1)
                {
                    CompletedAllTask?.Invoke();
                }
                else
                {
                    _config.Quantity -= 1;
                }
            }
        }
    }
}
