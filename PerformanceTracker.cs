using System.Diagnostics;


public class PerformanceTracker
{

    public delegate void Run();

    public double measuredRun = 0;

    public void Measure(Run run)
    {
        Stopwatch stopwatch = Stopwatch.StartNew();
        run();
        stopwatch.Stop();


        measuredRun = stopwatch.Elapsed.TotalMilliseconds;

    }



    public override string ToString()
    {

        return $"{measuredRun}ms";

    }



}

