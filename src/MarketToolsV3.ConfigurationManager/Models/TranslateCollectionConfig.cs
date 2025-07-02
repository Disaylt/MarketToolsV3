namespace MarketToolsV3.ConfigurationManager.Models;

public class TranslateCollectionConfig
{
    public Dictionary<string, TranslateView> Collection { get; set; } = [];
}

public class TranslateView
{
    public string? Ru { get; set; }
    public string? En { get; set; }
}