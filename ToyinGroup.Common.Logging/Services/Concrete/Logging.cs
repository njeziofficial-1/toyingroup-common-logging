namespace ToyinGroup.Common.Logging.Services.Concrete;
public class Logging : ISetUpAWSLogging
{
    public async Task SetUpAWSLogging(WebHostBuilderContext hostingContext, LoggerConfiguration loggerConfiguration)
    {
        var config = hostingContext.Configuration;
        var settings = config.GetSection("LoggingSettings").Get<LoggingSettings>();

        loggerConfiguration
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console();
        if (!string.IsNullOrEmpty(settings.CloudWatchLogGroup))
        {
            var options = new CloudWatchSinkOptions
            {
                LogGroupName = settings.CloudWatchLogGroup,
                CreateLogGroup = true,
                MinimumLogEventLevel = LogEventLevel.Information,
                TextFormatter = new CompactJsonFormatter(),
                RetryAttempts = 5,
                Period = TimeSpan.FromSeconds(10),
                QueueSizeLimit = 10000,
                BatchSizeLimit = 100
            };

            var awsOptions = config.GetAWSOptions();
            var cloudwatchClient = awsOptions.CreateServiceClient<IAmazonCloudWatchLogs>();
            loggerConfiguration
                .WriteTo
                .AmazonCloudWatch(options, cloudwatchClient);
        }
    }
}

