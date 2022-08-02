using System.Diagnostics;
using System.Text;

namespace ConnectK.Implementations
{
    internal class ImplementationRunner : IDisposable
    {
        internal ImplementationRunner(string runDescription, int planeSize, int k, int positionOfPlay)
        {
            Random random = new();
            bool[] boolBoard = new bool[planeSize];
            byte[] byteBoard = new byte[planeSize];
            StringBuilder stringBuilder = new();
            for (int i = 0; i < planeSize; i++)
            {
                int r = random.Next(0, 2);
                boolBoard[i] = Convert.ToBoolean(r);
                byteBoard[i] = Convert.ToByte(r);
                stringBuilder.Append(r);
            }

            RunDescription = runDescription;
            K = k;
            BoolBoard = boolBoard;
            ImplementationRuns = new List<ImplementationRun>();
            StringBoard = stringBuilder.ToString();
            PositionOfPlay = positionOfPlay;
            ByteBoard = byteBoard;
        }
        internal string RunDescription { get; set; }
        internal int K { get; set; }
        internal int PositionOfPlay { get; set; }
        internal bool[] BoolBoard { get; set; }
        internal byte[] ByteBoard { get; set; }
        internal string StringBoard { get; set; }
        internal List<ImplementationRun> ImplementationRuns { get; set; }

        public void Dispose()
        {
            GC.Collect();
        }

        internal void Run()
        {
            Winplementations winplementations = new(K);
            Stopwatch stopWatch = new();

            using (ImplementationRun implementationRun = new() { RunType = ImplementationType.boolArrayWithPositionAwareness })
            {
                stopWatch.Restart();
                implementationRun.WinResult = winplementations.boolWinplementation(BoolBoard, PositionOfPlay);
                stopWatch.Stop();
                implementationRun.TemporalPerformance = stopWatch.ElapsedTicks;
                ImplementationRuns.Add(implementationRun);
            }

            using (ImplementationRun implementationRun = new() { RunType = ImplementationType.boolArrayNoPositionRestrictor })
            {
                stopWatch.Restart();
                implementationRun.WinResult = winplementations.boolWinplementationWithoutPosition(BoolBoard);
                stopWatch.Stop();
                implementationRun.TemporalPerformance = stopWatch.ElapsedTicks;
                ImplementationRuns.Add(implementationRun);
            }

            using (ImplementationRun implementationRun = new() { RunType = ImplementationType.flatStringWithPositionAwareness })
            {
                stopWatch.Restart();
                implementationRun.WinResult = winplementations.stringWinplementation(StringBoard, PositionOfPlay);
                stopWatch.Stop();
                implementationRun.TemporalPerformance = stopWatch.ElapsedTicks;
                ImplementationRuns.Add(implementationRun);
            }

            using (ImplementationRun implementationRun = new() { RunType = ImplementationType.flatStringNoPositionRestrictor })
            {
                stopWatch.Restart();
                implementationRun.WinResult = winplementations.stringWinplementationWithoutPosition(StringBoard);
                stopWatch.Stop();
                implementationRun.TemporalPerformance = stopWatch.ElapsedTicks;
                ImplementationRuns.Add(implementationRun);
            }

            using (ImplementationRun implementationRun = new() { RunType = ImplementationType.bytePatternSearch })
            {
                stopWatch.Restart();
                implementationRun.WinResult = winplementations.byteSearchPattern(ByteBoard);
                stopWatch.Stop();
                implementationRun.TemporalPerformance = stopWatch.ElapsedTicks;
                ImplementationRuns.Add(implementationRun);
            }
        }
    }

    internal class ImplementationRun : IDisposable
    {
        internal bool WinResult { get; set; }
        internal long TemporalPerformance { get; set; }
        internal ImplementationType RunType { get; set; }

        public void Dispose()
        {
            GC.Collect();
        }
    }
    internal enum ImplementationType
    {
        boolArrayWithPositionAwareness = 0,
        boolArrayNoPositionRestrictor = 1,
        flatStringWithPositionAwareness = 2,
        flatStringNoPositionRestrictor = 3,
        bytePatternSearch = 4,
    }

    internal enum RunType
    {
        Control = 0,
        PlaneExtension = 1,
        PlaneAndMatchExtension = 2,
        PlaneExtremity = 3,
        PlaneAndMatchExtremity = 4
    }
}
