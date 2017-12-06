using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Toolchains.CsProj;
using BenchmarkDotNet.Toolchains.DotNetCli;

namespace one
{
    class MainConfig : ManualConfig
    {
        public MainConfig()
        {
            Add(Job.Default
                .With(Runtime.Core)
                .With(CsProjCoreToolchain.From(new NetCoreAppSettings(
                    targetFrameworkMoniker: "netcoreapp2.1",
                    runtimeFrameworkVersion: "2.1.0-preview1-25919-02",
                    customDotNetCliPath: @"E:\dotnet5\dotnet.exe",
                    name: "Core 2.1.0-preview1-25919-02")))
                .WithLaunchCount(1)
                .WithWarmupCount(1)
                .WithInvocationCount(1)
                .WithUnrollFactor(1)
                .WithTargetCount(10)
                .WithMaxRelativeError(0.01)
                .WithRemoveOutliers(true));

            Add(DefaultColumnProviders.Instance);
            Add(StatisticColumn.Median);
            Add(StatisticColumn.Min);
            Add(StatisticColumn.Max);
            Add(MarkdownExporter.GitHub);
            Add(new ConsoleLogger());
            Add(new HtmlExporter());
            Add(MemoryDiagnoser.Default);
        }
    }
}
