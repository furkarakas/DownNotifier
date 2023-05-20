using Serilog;

namespace DownNotifier.Middleware
{
    public static class LoggingExtensions
    {
        public static void AddSerilogConfiguration(this IHostBuilder builder)
        {
            Action<HostBuilderContext, IServiceProvider, LoggerConfiguration> ConfigureLogger =
                     (builderContext, serviceProvider, loggerConfiguration) =>
                                 loggerConfiguration.ReadFrom.Configuration(builderContext.Configuration)
                                            .Enrich.FromLogContext();

            builder.UseSerilog(ConfigureLogger);
        }
    }
}
