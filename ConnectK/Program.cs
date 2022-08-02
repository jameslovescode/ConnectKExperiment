using ConnectK;
using ConnectK.Implementations;

const int runs = 10;
List<RollupMetric> metrics = new List<RollupMetric>();

for (int i = 1; i < runs + 1; i++)
{
    bool doMetrics = (i > 2);

    using (ImplementationRunner implementationRunner = new(RunType.Control.ToString() + " #" + i, 10, 4, 5))
    {
        implementationRunner.Run();
        ConsoleWriter.WriteTitle(implementationRunner.RunDescription);
        foreach (ImplementationRun implementationRun in implementationRunner.ImplementationRuns)
        {
            ConsoleWriter.WriteRun(implementationRun);
            if(doMetrics)
                metrics.Add(new RollupMetric { implementationType = implementationRun.RunType, runSpeed = implementationRun.TemporalPerformance, runType = RunType.Control });
        }
    }

    using (ImplementationRunner implementationRunner = new(RunType.PlaneExtension.ToString() + " #" + i, 10000000, 4, 5000000))
    {
        implementationRunner.Run();
        ConsoleWriter.WriteTitle(implementationRunner.RunDescription);
        foreach (ImplementationRun implementationRun in implementationRunner.ImplementationRuns)
        {
            ConsoleWriter.WriteRun(implementationRun);
            if (doMetrics)
                metrics.Add(new RollupMetric { implementationType = implementationRun.RunType, runSpeed = implementationRun.TemporalPerformance, runType = RunType.PlaneExtension });
        }
    }

    using (ImplementationRunner implementationRunner = new(RunType.PlaneAndMatchExtension + " #" + i, 10000000, 5000, 5000000))
    {
        implementationRunner.Run();
        ConsoleWriter.WriteTitle(implementationRunner.RunDescription);
        foreach (ImplementationRun implementationRun in implementationRunner.ImplementationRuns)
        {
            ConsoleWriter.WriteRun(implementationRun);
            if (doMetrics)
                metrics.Add(new RollupMetric { implementationType = implementationRun.RunType, runSpeed = implementationRun.TemporalPerformance, runType = RunType.PlaneAndMatchExtension });
        }
    }

    using (ImplementationRunner implementationRunner = new(RunType.PlaneExtremity + " #" + i, 1000000000, 4, 500000000))
    {
        implementationRunner.Run();
        ConsoleWriter.WriteTitle(implementationRunner.RunDescription);
        foreach (ImplementationRun implementationRun in implementationRunner.ImplementationRuns)
        {
            ConsoleWriter.WriteRun(implementationRun);
            if (doMetrics)
                metrics.Add(new RollupMetric { implementationType = implementationRun.RunType, runSpeed = implementationRun.TemporalPerformance, runType = RunType.PlaneExtremity });
        }
    }

    using (ImplementationRunner implementationRunner = new(RunType.PlaneAndMatchExtremity + " #" + i, 1000000000, 500000, 500000000))
    {
        implementationRunner.Run();
        ConsoleWriter.WriteTitle(implementationRunner.RunDescription);
        foreach (ImplementationRun implementationRun in implementationRunner.ImplementationRuns)
        {
            ConsoleWriter.WriteRun(implementationRun);
            if (doMetrics)
                metrics.Add(new RollupMetric { implementationType = implementationRun.RunType, runSpeed = implementationRun.TemporalPerformance, runType = RunType.PlaneAndMatchExtremity });
        }
    }
}
ConsoleWriter.WriteAverageGroup(metrics, RunType.Control);
ConsoleWriter.WriteAverageGroup(metrics, RunType.PlaneExtension);
ConsoleWriter.WriteAverageGroup(metrics, RunType.PlaneAndMatchExtension);
ConsoleWriter.WriteAverageGroup(metrics, RunType.PlaneExtremity);
ConsoleWriter.WriteAverageGroup(metrics, RunType.PlaneAndMatchExtremity);
ConsoleWriter.WriteAverageSummary(metrics);