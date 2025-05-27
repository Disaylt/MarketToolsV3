using WB.Seller.Companies.Domain.Enums;

namespace WB.Seller.Companies.Application.Models;

public record PermissionDto
{
    public int Id { get; init; }
    public PermissionStatus Status { get; init; }
    public required string Path { get; init; }
}