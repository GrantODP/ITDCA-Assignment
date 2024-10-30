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

            int id = idTracker++;
            measuredRuns.Add(
             id,
               new Performace(stopwatch.ElapsedMilliseconds));

            return id;
        }

        public Performace GetPerformace(int id)
        {
            return measuredRuns[id];
        }

    }
}
