namespace ThreadVsTask.Workers;

public class ParallelForWork
{
    public static void Run(long iterations = 1_000_000)
    {
        Parallel.For(0, 10_000, _ =>
        {
            for (int dummy = 0; dummy < iterations; dummy++) ;
        });
    }
}