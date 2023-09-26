using Microsoft.Extensions.Hosting;
using Xunit.DependencyInjection;

namespace TestSkippableFact;
public static class Startup
{
    public static IHostBuilder CreateHostBuilder()
        => Host.CreateDefaultBuilder()
        .ConfigureAppConfiguration((context, configurationBuilder) =>
        {
        })
        .ConfigureServices((context, services) =>
        {
            services.AddSkippableFactSupport();
        })
        .UseConsoleLifetime();
}
