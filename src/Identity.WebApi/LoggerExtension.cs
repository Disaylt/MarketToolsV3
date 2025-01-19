using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Identity.Domain.Seed;
using MarketToolsV3.ConfigurationManager.Models;
using Serilog;
using Serilog.Events;

namespace Identity.WebApi
{
    public static class LoggerExtension
    {
        public static void AddLogging(this WebApplicationBuilder builder, ServiceConfiguration configuration)
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

            builder.Host.UseSerilog();
        }
    }
}
