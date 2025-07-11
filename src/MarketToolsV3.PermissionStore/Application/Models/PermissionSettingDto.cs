﻿using MarketToolsV3.PermissionStore.Application.Enums;

namespace MarketToolsV3.PermissionStore.Application.Models;

public record PermissionSettingDto
{
    public required string Path { get; init; }
    public PermissionStatusEnum Status { get; init; }
    public bool RequireUse { get; init; }
}