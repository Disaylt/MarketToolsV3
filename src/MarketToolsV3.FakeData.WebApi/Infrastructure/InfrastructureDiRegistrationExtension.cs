﻿using MarketToolsV3.FakeData.WebApi.Application.Features.Cookies.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Features.FakeTasks.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Features.ResponseBody.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Application.Features.TaskDetails.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Domain.Seed;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Database;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.Cookies.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.Cookies.Services.Implementation;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.FakeTasks.Services.Implementation;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.ResponseBody.Services.Implementation;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.ResponseBody.Utilities.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.ResponseBody.Utilities.Implementation;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Features.TaskDetails.Services.Implementation;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Repositories.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Repositories.Implementation;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Abstract;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Http.Implementation;
using MarketToolsV3.FakeData.WebApi.Infrastructure.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace MarketToolsV3.FakeData.WebApi.Infrastructure
{
    public static class InfrastructureDiRegistrationExtension
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection serviceCollection, ServiceConfig serviceConfig)
        {
            serviceCollection.AddNpgsql<FakeDataDbContext>(serviceConfig.DatabaseConnection);

            serviceCollection.AddDbContext<FakeDataDbContext>(opt =>
            {
                opt.UseNpgsql(serviceConfig.DatabaseConnection)
                    .UseSnakeCaseNamingConvention();
            });

            serviceCollection.AddScoped<ITaskEntityService, TaskEntityService>();
            serviceCollection.AddScoped<ITaskDetailsEntityService, TaskDetailsEntityService>();
            serviceCollection.AddScoped<ITaskDetailsHandleFacadeService, TaskDetailsHandleFacadeService>();
            serviceCollection.AddScoped<ITaskDetailsHttpBodyService, TaskDetailsHttpBodyService>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICookieEntityService, CookieEntityService>();
            serviceCollection.AddScoped<IResponseBodyTagService, ResponseBodyTagService>();
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped(typeof(IQueryableRepository<>), typeof(QueryableRepository<>));

            serviceCollection.AddSingleton<IMapperFactory, MapperFactory>();
            serviceCollection.AddSingleton<ICookieContainerBackgroundService, CookieContainerBackgroundService>();


            serviceCollection.AddSingleton<ITaskHttpClientFactory, TaskHttpClientFactory>();
            serviceCollection.AddSingleton<ITaskHttpLockStore, TaskHttpLockStore>();
            serviceCollection.AddSingleton<ITagIndexUtility, TagIndexUtility>();

            return serviceCollection;
        }
    }
}
