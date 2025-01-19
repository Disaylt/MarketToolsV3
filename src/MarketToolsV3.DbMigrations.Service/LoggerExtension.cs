using Microsoft.AspNetCore.Builder;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketToolsV3.DbMigrations.Service
{
    public static class LoggerExtension
    {
        public static void AddLogging(this HostApplicationBuilder builder)
        {
            LoggerConfiguration logConfig = new();

            logConfig = builder.Environment.IsDevelopment()
                ? logConfig.MinimumLevel.Debug()
                : logConfig.MinimumLevel.Information();

            Log.Logger = logConfig
                .Enrich.FromLogContext()
                .WriteTo.Console()
                //.WriteTo.Elasticsearch([new Uri(configuration.General.LogElasticConnection)], opts =>
                //{
                //    opts.MinimumLevel = builder.Environment.IsDevelopment() ? LogEventLevel.Debug : LogEventLevel.Information;
                //    opts.DataStream = new DataStreamName("logs", "generic", "identity-service");
                //    opts.BootstrapMethod = BootstrapMethod.Failure;
                //}, transport =>
                //{
                //    BasicAuthentication basicAuthentication = new(
                //        configuration.General.LogElasticUser,
                //            configuration.General.LogElasticPassword);
                //    transport
                //        .Authentication(basicAuthentication)
                //        .ServerCertificateValidationCallback(CertificateValidations.AllowAll);
                //})
                .CreateLogger();

            builder.Services.AddSerilog();
        }
    }
}
