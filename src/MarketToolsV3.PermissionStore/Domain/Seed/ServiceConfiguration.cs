using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.PermissionStore.Domain.Seed
{
    public class ServiceConfiguration
    {
        public string DatabaseConnection { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string PermissionsCollectionName { get; set; } = string.Empty;

        public ConfigTranslate TranslatePermissions { get; set; } = new();
    }

    public class ConfigTranslate
    {
        public Dictionary<string, string> Parameters { get; set; } = [];
    }
}
