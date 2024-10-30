using System.Diagnostics;

namespace GrantAssignment
{

    struct Performace
    {
        long RunTime { get; set; }
        public Performace(long measured)
        {
            RunTime = measured;
        }
    }

    class PerformanceTracker
    {

        public delegate void Run();

        Dictionary<int, Performace> measuredRuns = new Dictionary<int, Performace>();
        private int idTracker = 0;

        public int Measure(Run run)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            run();
            stopwatch.Stop();

            return idTracker++;
        }

    }
}
