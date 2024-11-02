using Elastic.Ingest.Elasticsearch;
using Elastic.Ingest.Elasticsearch.DataStreams;
using Elastic.Serilog.Sinks;
using Elastic.Transport;
using Identity.Domain.Seed;
using Serilog;
using Serilog.Events;

namespace Identity.WebApi
{
    public static class LoggerExtension
    {
        public static void AddLogging(this WebApplicationBuilder builder, IConfigurationSection serviceSection)
        {
            ServiceConfiguration config = serviceSection.Get<ServiceConfiguration>()
                                          ?? throw new NullReferenceException("Users config is empty");

            LoggerConfiguration logConfig = new LoggerConfiguration();

            logConfig = builder.Environment.IsDevelopment()
                ? logConfig.MinimumLevel.Debug()
                : logConfig.MinimumLevel.Information();

            Log.Logger = logConfig
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(new[] { new Uri(config.ElasticUrl) }, opts =>
                {
                    opts.MinimumLevel = LogEventLevel.Information;
                    opts.DataStream = new DataStreamName("logs", "console-example", "demo");
                    opts.BootstrapMethod = BootstrapMethod.Failure;
                }, transport =>
                {
                    transport.Authentication(new BasicAuthentication(config.ElasticUser, config.ElasticPassword))
                        .ServerCertificateValidationCallback(CertificateValidations.AllowAll);
                })
                .CreateLogger();

            builder.Host.UseSerilog();
        }
    }
}
