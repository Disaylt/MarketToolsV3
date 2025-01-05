using MarketToolsV3.ConfigurationManager.Abstraction;
using Microsoft.Extensions.Configuration;

namespace MarketToolsV3.ConfigurationManager.Implementations;

internal class ConfigManager(IConfigurationRoot configurationRoot) : IConfigManager
{
    protected IConfigurationRoot ConfigurationRoot { get; } = configurationRoot;

    public void JoinTo(IConfigurationManager applicationConfig)
    {
        applicationConfig.AddConfiguration(configurationRoot);
    }
}