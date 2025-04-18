﻿using System.Net;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;

namespace MarketToolsV3.FakeData.WebApi.Domain.Entities
{
    public class CookieEntity : Entity, IFromMap<Cookie>, IToMap<Cookie>
    {
        public required string Name { get; set; }
        public string? Value { get; set; }
        public string? Path { get; set; }
        public string? Domain { get; set; }


        public TaskEntity TaskEntity { get; set; } = null!;
        public string TaskId { get; set; } = null!;
    }
}
