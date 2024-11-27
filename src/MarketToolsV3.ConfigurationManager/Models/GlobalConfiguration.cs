using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.ConfigurationManager.Models
{
    public class GlobalConfiguration<T> where T : class
    {
        public GeneralConfiguration General {get; set; } = new GeneralConfiguration();
        public T? Service { get; set; }
    }
}
