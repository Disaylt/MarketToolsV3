namespace MarketToolsV3.PermissionStore.Application.Models;

public record PermissionSlimNodeDto
{
    public required string NextSegment { get; init; }
    public required string Permission { get; init; }
}