namespace ThreadVsTask.Workers;

public static class NativeThreadsWork
{
    public static void Run(long iterations = 1_000_000)
    {
        var threads = new Thread[10_000];
        for (int i = 0; i < 10_000; i++)
        {
            var t = new Thread(() =>
            {
                //Console.WriteLine(Environment.CurrentManagedThreadId);
                // ReSharper disable once EmptyForStatement
                for (var dummy = 0; dummy < iterations; dummy++) ;
            });
            t.Start(); //при высоком iterations, контекст начинает переключаться слишком часто
            threads[i] = t;
        }
        //Console.WriteLine("Threads created");
        foreach (var t in threads) t.Join();
    }

    public static void CreateThreadsSerialAndJoin(int count = 10_000)
    {
        for (var i = 0; i < count; i++)
        {
            var thread = new Thread(() => { });
            thread.Start();
            thread.Join();
        }
    }
    
    public static void CreateThreads(int count = 10_000)
    {
        for (var i = 0; i < count; i++)
        {
            var thread = new Thread(() => { });
            thread.Start();
        }
    }
    
    public static ManualResetEvent CreateThreadsAndWait(int count = 10_000)
    {
        var manualResetEvent = new ManualResetEvent(false);
        for (var i = 0; i < count; i++)
        {
            var thread = new Thread(() => manualResetEvent.WaitOne());
            thread.Start();
        }

        return manualResetEvent;
    }
}