using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using System.Threading.Tasks;
using Grpc.Net.Client;
using WB.Seller.Companies.Domain.Seed;
using WB.Seller.Companies.Infrastructure.Database;
using WB.Seller.Companies.Infrastructure.Repositories;
using WB.Seller.Companies.Infrastructure.Seed.Implementation;
using WB.Seller.Companies.Application.Models;
using WB.Seller.Companies.Application.QueryData.Companies;
using WB.Seller.Companies.Infrastructure.QueryDataHandlers.Companies;
using Npgsql;
using WB.Seller.Companies.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using WB.Seller.Companies.Application.QueryData.Permissions;
using WB.Seller.Companies.Infrastructure.QueryDataHandlers.Permissions;
using WB.Seller.Companies.Infrastructure.Utilities.Abstract;
using WB.Seller.Companies.Infrastructure.Utilities.Implementation;
using WB.Seller.Companies.Application.Services.Abstract;
using WB.Seller.Companies.Infrastructure.Services.Implementation;
using System.Net.Http;
using Polly;
using WB.Seller.Companies.Application.Seed;
using MarketToolsV3.ConfigurationManager.Models;
using MassTransit;
using MarketToolsV3.ConfigurationManager.Abstraction;
using MarketToolsV3.ConfigurationManager;

namespace WB.Seller.Companies.Infrastructure
{
    public static class RegistrationServicesExtension
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection collection,
            ServiceConfiguration serviceConfiguration)
        {
            collection.AddDbContext<WbSellerCompaniesDbContext>(opt =>
            {
                opt.UseNpgsql(serviceConfiguration.DatabaseConnection)
                    .UseSnakeCaseNamingConvention();
            });

            collection.AddScoped<IDbConnection>(_ => new NpgsqlConnection(serviceConfiguration.DatabaseConnection));

            collection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            collection.AddScoped<IUnitOfWork, EfCoreUnitOfWork<WbSellerCompaniesDbContext>>();

            collection.AddSingleton<IMapperFactory, MapperFactory>();
            collection.AddSingleton<IPermissionMapperUtility, PermissionMapperUtility>();

            collection.AddScoped<IQueryDataHandler<SlimCompanyRoleGroupsQueryData, IEnumerable<GroupDto<SubscriptionRole, CompanySlimInfoDto>>>, SlimCompanyRoleGroupsQueryDataHandler>();
            collection.AddScoped<IQueryDataHandler<SearchPermissionsQueryData, IEnumerable<PermissionDto>>, SearchPermissionsQueryDataHandler>();
            collection.AddScoped<IQueryDataHandler<SubscriptionAggregateQueryData, SubscriptionAggregateDto>, SubscriptionAggregateQueryDataHandler>();
            collection.AddScoped<IPermissionsExternalService, ExternalPermissionsService>();
            collection.AddSingleton<IQueryHandleService, QueryHandleService>();
            collection.AddScoped<IUserEntityService, UserEntityService>();

            collection.AddHttpClient("grpc")
                .AddStandardResilienceHandler(opt =>
                {
                    opt.Retry.MaxRetryAttempts = 4;
                    opt.Retry.Delay = TimeSpan.FromSeconds(2);
                    opt.Retry.BackoffType = DelayBackoffType.Exponential;
                });

            collection.AddSingleton(serviceProvider => new GrpcChannelOptions
            {
                HttpClient = serviceProvider
                    .GetRequiredService<IHttpClientFactory>()
                    .CreateClient("grpc"),
                DisposeHttpClient = false
            });

            collection.AddScoped<IPermissionsExternalService, ExternalPermissionsService>();

            SqlMapper.AddTypeHandler(new JsonTypeHandler<IEnumerable<PermissionDto>>());
            SqlMapper.AddTypeHandler(new JsonTypeHandler<IEnumerable<CompanySlimInfoDto>>());

            return collection;
        }

        public static async Task ConfigureBrokerMessenger(this IServiceCollection collection,
            ConfigurationServiceFactory configurationServiceFactory,
            Action<IBusRegistrationConfigurator>? busRegistrationConfigurator = null,
            Action<IBusRegistrationContext, IRabbitMqBusFactoryConfigurator>? rabbitMqBusFactoryConfigurator = null
            )
        {

            MessageBrokerConfig messageBrokerConfig = await configurationServiceFactory
                .CreateFromMessageBrokerAsync()
                .ContinueWith(x=> x.Result.Value);

            collection.AddMassTransit(mt =>
            {
                mt.AddEntityFrameworkOutbox<WbSellerCompaniesDbContext>(o =>
                {
                    o.UsePostgres();

                    o.UseBusOutbox();
                });

                busRegistrationConfigurator?.Invoke(mt);

                mt.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messageBrokerConfig.RabbitMqConnection,
                        "/",
                        h =>
                        {
                            h.Username(messageBrokerConfig.RabbitMqLogin);
                            h.Password(messageBrokerConfig.RabbitMqPassword);
                        });

                    rabbitMqBusFactoryConfigurator?.Invoke(context, cfg);

                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
