using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Abstraction
{
    public interface ITypingConfigManager<out T>
        : IConfigManager
        where T : class
    {
        public T Value { get; }
        public void AddAsOptions(IServiceCollection services);
    }
}
