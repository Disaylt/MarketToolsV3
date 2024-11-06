using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Core.Logging;
using Identity.Application.Commands;
using Microsoft.Extensions.Logging;

namespace MarketToolsV3.Users.UnitTests.Source
{
    internal class Logging
    {
        public static ILogger<T> Create<T>()
        {
            return new Logger<T>(LoggerFactory.Create(x => x.AddConsole()));
        }
    }
}
