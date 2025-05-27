namespace MarketToolsV3.ConfigurationManager.Models;

public class ModulesInfoConfig
{
    public required ModulesInfoMarketplaces Marketplaces { get; set; }
}

public class ModulesInfoMarketplaces
{
    public required string Wb { get; set; }
}