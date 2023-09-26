using System.Reflection;
using TestSkippableFact;
using Xunit.Runners;

internal class Program
{
    private static async Task Main(string[] args)
    {
        using var host = Startup
        .CreateHostBuilder()
        .Build();

        await host.StartAsync();

        using var runner = AssemblyRunner.WithoutAppDomain(Assembly.GetEntryAssembly()!.Location);
        runner.OnTestFailed = OnTestFailed;
        runner.OnTestSkipped = OnTestSkipped;
        runner.Start();

        using var cts = new CancellationTokenSource(TimeSpan.FromMinutes(2));

        while (runner.Status != AssemblyRunnerStatus.Idle)
        {
            if (cts.IsCancellationRequested)
            {
                Console.WriteLine($"Test runnner did not transition to idle 2 minutes after running test.");
                break;
            }

            await Task.Delay(100);
        }
    }

    private static void OnTestSkipped(TestSkippedInfo info)
    {
        Console.WriteLine($"Test skipped {info.MethodName}");
    }

    private static void OnTestFailed(TestFailedInfo info)
    {
        Console.WriteLine($"Test failed {info.MethodName}");
    }
}