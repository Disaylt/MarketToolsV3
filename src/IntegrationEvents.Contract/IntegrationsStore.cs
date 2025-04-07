using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationEvents.Contract
{
    public class IntegrationsStore
    {
        public static Dictionary<string, Type> FullNameAndTypePairs { get; } = Assembly
            .GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsClass 
                        && typeof(BaseIntegrationEvent).IsAssignableFrom(t) 
                        && string.IsNullOrEmpty(t.FullName) == false)
            .ToDictionary(x => x.FullName!);
    }
}
