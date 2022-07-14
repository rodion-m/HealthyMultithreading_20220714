namespace ThreadVsTask.Workers;

public static class TasksWork
{
    public static Task RunAsync(long iterations = 1_000_000)
    {
        var tasks = new List<Task>(10_000);
        for (int i = 0; i < 10_000; i++)
        {
            var t = Task.Run(() =>
            {
                //Console.WriteLine(Environment.CurrentManagedThreadId);
                // ReSharper disable once EmptyForStatement
                for (var dummy = 0; dummy < iterations; dummy++) ;
            });
            tasks.Add(t);
        }
        //Console.WriteLine("Tasks queued");
        return Task.WhenAll(tasks);
    }
}