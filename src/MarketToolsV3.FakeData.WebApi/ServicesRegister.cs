using Serilog;
using Serilog.Sinks.SystemConsole.Themes;

namespace MarketToolsV3.FakeData.WebApi
{
    public static class ServicesRegister
    {
        public static void AddFakeDataLogging(this WebApplicationBuilder builder)
        {
            LoggerConfiguration logConfig = new();

            logConfig = builder.Environment.IsDevelopment()
                ? logConfig.MinimumLevel.Debug()
                : logConfig.MinimumLevel.Information();

            Log.Logger = logConfig
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
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
