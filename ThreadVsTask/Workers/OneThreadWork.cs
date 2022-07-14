namespace ThreadVsTask.Workers;

public static class OneThreadWork
{
    public static void Run(long iterations = 1_000_000)
    {
        for (int i = 0; i < 10_000; i++)
        {
            for (var dummy = 0; dummy < iterations; dummy++);
        }
    }
}