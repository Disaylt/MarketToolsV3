﻿using MarketToolsV3.FakeData.WebApi.Domain.Entities;

namespace MarketToolsV3.FakeData.WebApi.Application.Services.Abstract
{
    public interface ITaskDetailsEntityService
    {
        Task<TaskDetails?> TakeNextAsync(string taskId);
        Task SetGroupAsSkipAsync(string taskId, int groupId);
    }
}
