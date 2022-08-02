using ConnectK.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectK
{
    internal static class ConsoleWriter
    {
        internal static void WriteTitle(string title)
        {
            Console.WriteLine("==================================");
            Console.WriteLine(title);
            Console.WriteLine("----------------------------------");

        }
        internal static void WriteRun(ImplementationRun implementationRun)
        {
            Console.WriteLine("TYPE    = " + implementationRun.RunType.ToString());
            Console.WriteLine("RESULT  = " + implementationRun.WinResult.ToString());
            Console.WriteLine("LENGTH  = " + implementationRun.TemporalPerformance.ToString());
        }
        internal static void WriteAverageSummary(List<RollupMetric> metrics)
        {
            WriteTitle("SUMMARY OF AVERAGES IN ALL TESTS");
            Console.WriteLine(ImplementationType.boolArrayWithPositionAwareness.ToString() + ": " + String.Format("{0:n0}", metrics.Where(m => m.implementationType == ImplementationType.boolArrayWithPositionAwareness).Average(s => s.runSpeed)));
            Console.WriteLine(ImplementationType.boolArrayNoPositionRestrictor.ToString() + ": " + String.Format("{0:n0}", metrics.Where(m => m.implementationType == ImplementationType.boolArrayNoPositionRestrictor).Average(s => s.runSpeed)));
            Console.WriteLine(ImplementationType.flatStringWithPositionAwareness.ToString() + ": " + String.Format("{0:n0}", metrics.Where(m => m.implementationType == ImplementationType.flatStringWithPositionAwareness).Average(s => s.runSpeed)));
            Console.WriteLine(ImplementationType.flatStringNoPositionRestrictor.ToString() + ": " + String.Format("{0:n0}", metrics.Where(m => m.implementationType == ImplementationType.flatStringNoPositionRestrictor).Average(s => s.runSpeed)));
            Console.WriteLine(ImplementationType.bytePatternSearch.ToString() + ": " + String.Format("{0:n0}", metrics.Where(m => m.implementationType == ImplementationType.bytePatternSearch).Average(s => s.runSpeed)));
        }
        internal static void WriteAverageGroup(List<RollupMetric> metrics, RunType runType)
        {
            WriteTitle("SUMMARY OF AVERAGES IN " + runType.ToString() + " TESTS");
            WriteAverage(metrics, runType, ImplementationType.boolArrayWithPositionAwareness);
            WriteAverage(metrics, runType, ImplementationType.boolArrayNoPositionRestrictor);
            WriteAverage(metrics, runType, ImplementationType.flatStringWithPositionAwareness);
            WriteAverage(metrics, runType, ImplementationType.flatStringNoPositionRestrictor);
            WriteAverage(metrics, runType, ImplementationType.bytePatternSearch);
        }
        internal static void WriteAverage(List<RollupMetric> metrics, RunType runType, ImplementationType implementationType)
        {
            Console.WriteLine(implementationType.ToString() + ": " + String.Format("{0:n0}", metrics.Where(m => m.runType == runType && m.implementationType == implementationType).Average(s => s.runSpeed)));
        }
    }
}
